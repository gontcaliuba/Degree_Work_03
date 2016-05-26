using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DegreeWork_01
{
    public partial class Form3 : Form
    {
        // WaveIn - поток для записи
        WaveIn waveIn;
        //Класс для записи в файл
        WaveFileWriter writer;
        //Имя файла для записи
        string outputFilename = "01.wav";
        RemindRange remindList = new RemindRange();
        bool isRecording = false;

        public Form3()
        {
            InitializeComponent();

            Dictionary<int, string> comboSource = new Dictionary<int, string>();
            comboSource.Add(0, "00:00");
            comboSource.Add(1, "01:00");
            comboSource.Add(2, "02:00");
            comboSource.Add(3, "03:00");
            comboSource.Add(4, "04:00");
            comboSource.Add(5, "05:00");
            comboSource.Add(6, "06:00");
            comboSource.Add(7, "07:00");
            comboSource.Add(8, "08:00");
            comboSource.Add(9, "09:00");
            comboSource.Add(10, "10:00");
            comboSource.Add(11, "11:00");
            comboSource.Add(12, "12:00");
            comboSource.Add(13, "13:00");
            comboSource.Add(14, "14:00");
            comboSource.Add(15, "15:00");
            comboSource.Add(16, "16:00");
            comboSource.Add(17, "17:00");
            comboSource.Add(18, "18:00");
            comboSource.Add(19, "19:00");
            comboSource.Add(20, "20:00");
            comboSource.Add(21, "21:00");
            comboSource.Add(22, "22:00");
            comboSource.Add(23, "23:00");

            timeList.DataSource = new BindingSource(comboSource, null);
            timeList.DisplayMember = "Value";
            timeList.ValueMember = "Key";

            RemindMessage.KeyDown += Form3_KeyDown;
            RemindMessage.KeyUp += Form3_KeyUp;
            dateAndTime.KeyDown += Form3_KeyDown;
            dateAndTime.KeyUp += Form3_KeyUp;
            timeList.KeyDown += Form3_KeyDown;
            timeList.KeyUp += Form3_KeyUp;
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
            string command = JsonWorker.Convert(result);
            DateTime dateAndTimeOfRemind = dateAndTime.Value.Date;
            string timeInStr = timeList.Text.Substring(0, 2);
            int timeInInt = Int32.Parse(timeInStr);
            TimeSpan time = new TimeSpan(timeInInt, 0, 0);
            dateAndTimeOfRemind += time;
            Remind remind = new Remind(remindList.remindList.Count, dateAndTimeOfRemind, command);
            setData(remind);
            Form5 saveForm = new Form5("Сохранить напоминание?\nНажмите Enter для сохранения.\nНажмите пробел для отмены.", remind, false);
            saveForm.ShowDialog();
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
        public void setData(Remind remindToShow)
        {
            dateAndTime.Value = remindToShow.getDateTime();
            RemindMessage.Text = remindToShow.getMessage();
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Space) return;
            if (isRecording == true) return;
            Image myimage = new Bitmap("isRecording_03.jpg");
            this.BackgroundImage = myimage;
            start();
        }

        void Form3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Space) return;
            Image myimage = new Bitmap("isntRecording_03.jpg");
            this.BackgroundImage = myimage;
            stop();
        }

        private void dateAndTime_ValueChanged(object sender, EventArgs e)
        {
            RemindMessage.Focus();
        }
    }
}
