using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DegreeWork_01
{
    public partial class ReminListForm : Form
    {
        // WaveIn - поток для записи
        WaveIn waveIn;
        //Класс для записи в файл
        WaveFileWriter writer;
        //Имя файла для записи
        string outputFilename = "01.wav";
        RemindRange reminds = new RemindRange();
        bool isRecording = false;
        public ReminListForm()
        {
            InitializeComponent();
            setData();

            remindTextBox.KeyDown += Form5_KeyDown;
            remindTextBox.KeyUp += Form5_KeyUp;
            this.KeyDown += Form5_KeyDown;
            this.KeyUp += Form5_KeyUp;
        }

        private void setData()
        {
            string remindList = null;
            for (int i = 0; i < reminds.remindList.Count; i++)
            {
                remindList += reminds.remindList[i].getId().ToString() + 
                    "   " + reminds.remindList[i].getMessage() + "\n";
            }
            remindTextBox.Text = remindList;
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

        public void commandProcessing()
        {
            var stream = new MemoryStream(File.ReadAllBytes("01.wav"));
            string result = SpeechRecognizer.WavStreamToGoogle(stream);
            int remindId = JsonWorker.getNumber(result);
            if (remindId == -1)
            {
                return;
            }
            // Проверяем, число ли в строке, на выходе значение для remindId
            AgreeForm deleteForm = new AgreeForm("Вы действительно хотите удалить напоминание под номером " + remindId.ToString() + "? \n\nНажмите Enter для удаления.\nНажмите пробел для отмены.",
                reminds.getRemindById(remindId), true);
            deleteForm.ShowDialog();
            this.Close();
        }
        //Начинаем запись
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

        private void Form5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Space) return;
            if (isRecording == true) return;
            Image myimage = new Bitmap("isRecording_04.jpg");
            this.BackgroundImage = myimage;
            start();
        }

        void Form5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Space) return;
            Image myimage = new Bitmap("isntRecording_04.jpg");
            this.BackgroundImage = myimage;
            stop();
        }
    }
}
