﻿using NAudio.Wave;
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
    public partial class MainForm : Form
    {
        // WaveIn - поток для записи
        WaveIn waveIn;
        //Класс для записи в файл
        WaveFileWriter writer;
        //Имя файла для записи
        string outputFilename = "01.wav";
        bool isRecording = false;

        public MainForm()
        {
            InitializeComponent();

            RemindControl remindChecking = new RemindControl();

            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
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
            string command = JsonWorker.Convert(result);
            Engine eng = new Engine();
            eng.commandsHandler(command);
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Space) return;
            if (isRecording == true) return;
            Image myimage = new Bitmap("isRecording.jpg");
            this.BackgroundImage = myimage;
            start();
        }

        void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Space) return;
            Image myimage = new Bitmap("isntRecording.jpg");
            this.BackgroundImage = myimage;
            stop();
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void свернутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
