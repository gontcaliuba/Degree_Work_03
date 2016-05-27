using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DegreeWork_01
{
    public partial class Form2 : Form
    {
        // WaveIn - поток для записи
        WaveIn waveIn;
        //Класс для записи в файл
        WaveFileWriter writer;
        //Имя файла для записи
        string outputFilename = "01.wav";
        bool isRecording = false;
        bool isVideoCall = false;
        List<ContactSkype> contacts;
        ContactList contactsList = new ContactList();
        public Form2(bool isVideo)
        {
            isVideoCall = isVideo;
            InitializeComponent();
            contacts = contactsList.getContacts();
            string contactList = null;
            for (int i = 0; i < contacts.Count; i++)
            {
                contactList += contacts[i].getID().ToString() + "   " + contacts[i].getName() + "\n";
            }
            richTextBox1.Text = contactList;

            richTextBox1.KeyDown += Form2_KeyDown;
            richTextBox1.KeyUp += Form2_KeyUp;

            this.KeyDown += Form2_KeyDown;
            this.KeyUp += Form2_KeyUp;
        }

        //Получение данных из входного буфера и обработка полученных с микрофона данных
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
            }
            else
            {
                //Записываем данные из буфера в файл
                writer.WriteData(e.Buffer, 0, e.BytesRecorded);
            }
        }
        //Завершаем запись
        void StopRecording()
        {
            //MessageBox.Show("StopRecording");
            waveIn.StopRecording();

        }
        //Окончание записи
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
            }
            else
            {
                waveIn.Dispose();
                waveIn = null;
                writer.Close();
                writer = null;
                commandProcessing();
            }
        }

        private void commandProcessing()
        {
            var stream = new MemoryStream(File.ReadAllBytes("01.wav"));
            string result = SpeechRecognizer.WavStreamToGoogle(stream);
            int nameId = JsonWorker.getNumber(result);
            Engine eng = new Engine();
            if (nameId == -1)
            {
                return;
            }
            // Проверяем, число ли в строке, на выходе значение для intId
            eng.skypeCalls(isVideoCall, contactsList.getNameById(nameId));
            this.Close();
        }
        private void start()
        {
            isRecording = true;
            try
            {
                //MessageBox.Show("Start Recording");
                waveIn = new WaveIn();
                //Дефолтное устройство для записи (если оно имеется)
                waveIn.DeviceNumber = 0;
                //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                waveIn.DataAvailable += waveIn_DataAvailable;
                //Прикрепляем обработчик завершения записи
                waveIn.RecordingStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(waveIn_RecordingStopped);
                //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                waveIn.WaveFormat = new WaveFormat(22050, 1);
                //Инициализируем объект WaveFileWriter
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                //Начало записи
                waveIn.StartRecording();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        //Прерываем запись
        private void stop()
        {
            if (waveIn != null)
            {
                StopRecording();
                isRecording = false;
            }
        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Space) return;
            if (isRecording == true) return;
            Image myimage = new Bitmap("isRecording_02.jpg");
            this.BackgroundImage = myimage;
            start();
        }

        void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Space) return;
            Image myimage = new Bitmap("isntRecording_02.jpg");
            this.BackgroundImage = myimage;
            stop();
        }
    }
}
