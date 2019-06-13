using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using FastColoredTextBoxNS;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class ExMain:Form
    {
        [DllImport("user32.dll")]//winapi 
        static extern bool HideCaret(IntPtr hWnd);//для скрытия курсора
        public ExMain()
        {
           InitializeComponent();
           richTextBox1.Cursor = Cursors.Arrow; // изменение курсора для элемента на обычный
        }
        
        bool gLoad = false;//глобальная переменная как флаг того что в rtb текст изменился только от загрузки туда инфе
        private void Form2_Load(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem.Enabled = false;
            сохранитьКакToolStripMenuItem.Enabled = false;
            отменаДействияToolStripMenuItem.Enabled = false;
            восстановитьToolStripMenuItem.Enabled = false;
            закрытьToolStripMenuItem.Enabled = false;
            скомпилироватьToolStripMenuItem.Enabled = false;
            выполнитьToolStripMenuItem1.Enabled = false;
            скомпилироватьИВыполнитьToolStripMenuItem.Enabled = false;
            копироватьToolStripMenuItem.Enabled = false;
            вставкаToolStripMenuItem.Enabled = false;
            выделитьВсёToolStripMenuItem.Enabled = false;
            вВерхнийРегистрToolStripMenuItem.Enabled = false;
            вНижнийРегистрToolStripMenuItem.Enabled = false;
            вставитьубратьКомментарииToolStripMenuItem.Enabled = false;
            режимВставкиИЗаменыToolStripMenuItem.Enabled = false;
            убратьТекущуюСтрокуToolStripMenuItem.Enabled = false;
            найтиToolStripMenuItem.Enabled = false;
            заменитьToolStripMenuItem.Enabled = false;
            перейтиКСтрокеToolStripMenuItem.Enabled = false;
            приблизитьToolStripMenuItem.Enabled = false;
            отдалитьToolStripMenuItem.Enabled = false;
            вырезатьToolStripMenuItem.Enabled = false;
            добавитьЗакладкуToolStripMenuItem.Enabled = false;
            убратьЗакладкуToolStripMenuItem.Enabled = false;
            перейтиКЗакладкеToolStripMenuItem.Enabled = false;
            перейтиКЗакладкеToolStripMenuItem1.Enabled = false;

            toolStripSaveF.Enabled = false;
            toolStripCloseF.Enabled = false;
            toolStripFind.Enabled = false;
            toolStripZoomPlus.Enabled = false;
            toolStripZoomMinus.Enabled = false;
            toolStripCompRum.Enabled = false;

        }
        public void Page()
        {//Добавление страницы с richtexbox'ом,контекстным меню
            TabPage tp = new TabPage();//новая страница
            int tc = (metroTabControl1.TabCount + 1);//счётчик для названия
            tp.Text = "Unnamed " + tc.ToString();//название
            tp.Tag = "";//тег для хранения пути,"обнулён"
            metroTabControl1.TabPages.Add(tp);//добавление страницы
            FastColoredTextBox rtb = new FastColoredTextBox();
            rtb.Dock = DockStyle.Fill;//заполнение места
            rtb.Name = "rtb";
            rtb.Tag = "";//тег для хранения пути,"обнулён"
            rtb.AcceptsTab = true;//tab
            rtb.ContextMenuStrip = metroContextMenu1;//меню для возможности закрытия док-та
            rtb.TextChanged += new EventHandler<TextChangedEventArgs>(rtb_TextChanged);//делегат для события textchanged
            rtb.SelectionChanged += new EventHandler(rtb_SelectionChanged);//делегат для события SelectionChanged
            rtb.UndoRedoStateChanged += new EventHandler<EventArgs>(rtb_UndoRedoStateChanged);//делегат для события UndoRedoStateChanged
            rtb.Language = FastColoredTextBoxNS.Language.CSharp;//выбор языка c#
            rtb.AllowMacroRecording = false;//макросы выключены
            rtb.WordWrap = true;//перенос строк
            rtb.CurrentLineColor = System.Drawing.Color.DeepSkyBlue;//подсветка текущей строки
            rtb.MouseDoubleClick += Rtb_MouseDoubleClick;   
            tp.Controls.Add(rtb);//доб-е richtextbox'a в страницу
            metroTabControl1.SelectedTab = tp;//новая страница сразу становится активной(выбранной)
            сохранитьКакToolStripMenuItem.Enabled = true;//сохранить как включается так как есть хотя бы страница
            закрытьToolStripMenuItem.Enabled = true;//закрыть включается так как есть что закрыть
            скомпилироватьToolStripMenuItem.Enabled = true;//включение кнопок
            выполнитьToolStripMenuItem1.Enabled = true;//отвечающих за
            скомпилироватьИВыполнитьToolStripMenuItem.Enabled = true;//компиляцию,запуск
            вставкаToolStripMenuItem.Enabled = true;//страница есть значит вставка разблокируется
            выделитьВсёToolStripMenuItem.Enabled = true;
            вВерхнийРегистрToolStripMenuItem.Enabled = true;
            вНижнийРегистрToolStripMenuItem.Enabled = true;
            вставитьубратьКомментарииToolStripMenuItem.Enabled = true;
            режимВставкиИЗаменыToolStripMenuItem.Enabled = true;
            убратьТекущуюСтрокуToolStripMenuItem.Enabled = true;
            найтиToolStripMenuItem.Enabled = true;
            заменитьToolStripMenuItem.Enabled = true;
            перейтиКСтрокеToolStripMenuItem.Enabled = true;
            приблизитьToolStripMenuItem.Enabled = true;
            отдалитьToolStripMenuItem.Enabled = true;
            добавитьЗакладкуToolStripMenuItem.Enabled = true;
            убратьЗакладкуToolStripMenuItem.Enabled = true;
            перейтиКЗакладкеToolStripMenuItem.Enabled = true;
            перейтиКЗакладкеToolStripMenuItem1.Enabled = true;
            toolStripCloseF.Enabled = true;
            toolStripFind.Enabled = true;
            toolStripZoomPlus.Enabled = true;
            toolStripZoomMinus.Enabled = true;
            toolStripCompRum.Enabled = true;
        }

        private void Rtb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (e.X < selectedRtb.LeftIndent)//если клик был по левой зоне компонента(до текста,в зоне строк)
                {
                    var place = selectedRtb.PointToPlace(e.Location);//запись места клика
                    if (selectedRtb.Bookmarks.Contains(place.iLine))//если закладка есть убирает,нет - ставит
                        selectedRtb.Bookmarks.Remove(place.iLine);
                    else
                        selectedRtb.Bookmarks.Add(place.iLine);
                }
            }
        }

        void rtb_TextChanged(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (gLoad == true)
                {
                    gLoad = false;//event вызван загрузкой текста из файла,поэтому ничего делать не надо
                    selectedRtb.IsChanged = false;//изменения сброшены
                    return;//просто выход из метода,предварительно инверсировав флаг(для возможности индикации будущих
                           //загрузок 
                }
                if (selectedRtb.IsChanged == true)//если modified true()
                {                                //то кнопки сохранить разблок,если нет то блок кнопок  ;
                    сохранитьToolStripMenuItem.Enabled = true;
                    toolStripSaveF.Enabled = true;
                }
                else
                {
                    сохранитьToolStripMenuItem.Enabled = false;
                    toolStripSaveF.Enabled = false;
                }
            }

        }
        void rtb_SelectionChanged(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (selectedRtb.SelectionLength > 0)//если выделеляется больше 0 символов то кнопка копировать разблок
                {
                    копироватьToolStripMenuItem.Enabled = true;
                    вырезатьToolStripMenuItem.Enabled = true;
                }
                else
                {
                    копироватьToolStripMenuItem.Enabled = false;
                    вырезатьToolStripMenuItem.Enabled = false;
                }
            }
        }
        void rtb_UndoRedoStateChanged(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (selectedRtb.UndoEnabled == true)//блок,разблок кнопки в зав-ти от готовности undo(отмена)
                {
                    отменаДействияToolStripMenuItem.Enabled = true;
                }
                else
                {
                    отменаДействияToolStripMenuItem.Enabled = false;
                }
                if (selectedRtb.RedoEnabled == true)//блок,разблок кнопки в зав-ти от готовности redo(восстановление)
                {
                    восстановитьToolStripMenuItem.Enabled = true;
                }
                else
                {
                    восстановитьToolStripMenuItem.Enabled = false;
                }

            }
        }
        public void Save()
        {
            //проверка на существование активной страницы(если её нет то нет страниц вообще и сохранять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //selt.tag хранит в себе путь сохранения файла
                //selectedRtb хранит в себе путь открытия файла
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (File.Exists(selT.Tag.ToString()))
                {
                    File.WriteAllText(selT.Tag.ToString(), selectedRtb.Text, Encoding.UTF8);
                }
                else
                {
                    if (File.Exists(selectedRtb.Tag.ToString()))
                    {
                        File.WriteAllText(selectedRtb.Tag.ToString(), selectedRtb.Text, Encoding.UTF8);
                    }
                    else
                    {
                        SaveAs();//если по предыдущим путям ничего не найдено то предлагается сохранить;
                    }
                }
                toolStripSaveF.Enabled = false;//выключение кнопок сохранения
                сохранитьToolStripMenuItem.Enabled = false;//
                selectedRtb.IsChanged = false;//флаг того,что текст модифицирован убран
            }

        }
        public void SaveAs()
        {
            //проверка на существование активной страницы(если её нет то нет страниц вообще и сохранять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                //показ имени файла без полного пути
                saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //selt.tag хранит в себе путь сохранения файла
                    //selectedRtb хранит в себе путь открытия файла
                    //создание 2 переменных типа tabpage,richtextbox и заполнение их данными
                    //с текущей  страницы,для того чтобы к элементами на этой странице можно было обратиться
                    //последующая чтение richtextbox'a и запись в file
                    //проверка на существование активной страницы(если её нет то нет страниц вообще и сохранять нечего)
                    //сохранение пути файла(текущей страницы) через tag
                    selT.Tag = saveFileDialog1.FileName.ToString();
                    File.WriteAllText(selT.Tag.ToString(), selectedRtb.Text, Encoding.UTF8);
                    selT.Text = Path.GetFileName(saveFileDialog1.FileName);//имя вкладки=название файла
                    toolStripSaveF.Enabled = false;//выключение кнопок сохранения
                    сохранитьToolStripMenuItem.Enabled = false;//
                    selectedRtb.IsChanged = false;//флаг того,что текст модифицирован убран
                }
            }
        }
        public void OpenFile()
        {
            //показ имени файла без полного пути
            openFileDialog1.FileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //selt.tag хранит в себе путь сохранения файла
                //selectedRtb хранит в себе путь открытия файла
                //создание 2 переменных типа tabpage,richtextbox и заполнение их данными
                //с текущей  страницы,для того чтобы к элементами на этой странице можно было обратиться
                //последующая чтение file'a и запись в richtexbox
                //сравнение пути открываемого файла и записей путей из сохранения и открытия разных tab'ob и их rtb
                //если совпадают то просто переключает на нужную страницу,нет то создаёт новую
                foreach (TabPage page in metroTabControl1.TabPages)
                {
                    if (page.Tag.ToString() == openFileDialog1.FileName)
                    {
                        metroTabControl1.SelectTab(page);
                        return;
                    }
                    if (page.Controls["rtb"].Tag.ToString() == openFileDialog1.FileName)
                    {
                        metroTabControl1.SelectTab(page);
                        return;
                    }
                }
                Page();//создание страницы
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"]; ;
                selectedRtb.Tag = openFileDialog1.FileName.ToString();
                gLoad = true;//флаг показывающий что изменения в тексте вызваны загрузкой текста
                selectedRtb.Text = File.ReadAllText(selectedRtb.Tag.ToString(), Encoding.UTF8);//выгрузка текста в rtb
                selT.Text = Path.GetFileName(openFileDialog1.FileName);//имя вкладки=название файла
            }
        }
        public void CompileWithRun()
        {
            //проверка на существование активной страницы(если её нет то и данAных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if ((File.Exists(selT.Tag.ToString())) || (File.Exists(selectedRtb.Tag.ToString())))//Проверка на существование путей
                {                                                                                   //из сохранения или открытия
                    string path;
                    //путь файла
                    //запись пути из сохранения если нет то из открытия
                    if (File.Exists(selT.Tag.ToString()))
                    {
                        path = selT.Tag.ToString();
                    }
                    else
                    {
                        path = selectedRtb.Tag.ToString();
                    }
                    //получение пути системы,выделение от туда буквы системного диска
                    string sys = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
                    string sys2 = Regex.Replace(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), @":\\", "");
                    //Выделение буквы логического диска на котором хранится файл с программным кодом
                    string letter = Regex.Replace((Path.GetPathRoot(path)), @":\\", "");
                    //директория сохранённого файла
                    string dir = Path.GetDirectoryName(path);
                    //имя сохранённого файла
                    string name = Path.GetFileName(path);
                    //имя сохранённого файла без расширения
                    string namex = Path.GetFileNameWithoutExtension(path);
                    //диск на котором программа
                    string prog = Path.GetPathRoot(Application.StartupPath);
                    //Путь к компилятору + имя файла
                    string compiler = $@"{sys2}:\Windows\Microsoft.NET\Framework\v3.5\csc.exe";
                    //запуск компилятора
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = $"{compiler}";
                        process.StartInfo.WorkingDirectory = dir;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardInput = true;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.Arguments = $@"/out:{namex}.exe  /nologo {path}";
                        process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                        process.Start();
                        string outs;//output для csc.exe
                        outs = process.StandardOutput.ReadToEnd().ToString();//запись output
                        richTextBox1.Text = outs;//вывод output'a
                        //ожидание окончания процесса
                        process.WaitForExit();
                        if (outs == "")//если outs "",значит ошибок не было и можно запускать
                        {
                            Run();//метод для запуска exe
                        }
                    }
                }
                else
                {
                    SaveAs();
                }
            }
        }
        public void Run()
        {//проверка на существование активной страницы(если её нет то и данных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                //selt.tag хранит в себе путь сохранения файла
                //selectedRtb хранит в себе путь открытия файла
                string save, open;
                //замена расширения для запуска exe
                save = selT.Tag.ToString().Replace(".cs", ".exe");
                open = selectedRtb.Tag.ToString().Replace(".cs", ".exe");
                //сначала проверка файла из сохранения если нет,то из открытия
                if (File.Exists($@"{save}"))
                {
                    using (Process myProcess = new Process())
                    {
                        myProcess.StartInfo.UseShellExecute = false;
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.StartInfo.FileName = $@"{save}";
                        myProcess.Start();
                    }
                }
                else
                {
                    if (File.Exists($@"{open}"))
                    {
                        using (Process myProcess = new Process())
                        {
                            myProcess.StartInfo.UseShellExecute = false;
                            myProcess.StartInfo.CreateNoWindow = false;
                            myProcess.StartInfo.FileName = $@"{open}";
                            myProcess.Start();
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show($"Исполняемый файл не скомпилирован.{System.Environment.NewLine}Скомпилировать сейчас?", "Потверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            CompileWithRun();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                        }
                    }
                }
            }
        }
        public void Compile()
        {
            //проверка на существование активной страницы(если её нет то и данAных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if ((File.Exists(selT.Tag.ToString())) || (File.Exists(selectedRtb.Tag.ToString())))//Проверка на существование путей
                {                                                                                   //из сохранения или открытия
                    string path;
                    //путь файла
                    //запись пути из сохранения если нет то из открытия
                    if (File.Exists(selT.Tag.ToString()))
                    {
                        path = selT.Tag.ToString();
                    }
                    else
                    {
                        path = selectedRtb.Tag.ToString();
                    }
                    //получение пути системы,выделение от туда буквы системного диска
                    string sys = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
                    string sys2 = Regex.Replace(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), @":\\", "");
                    //Выделение буквы логического диска на котором хранится файл с программным кодом
                    string letter = Regex.Replace((Path.GetPathRoot(path)), @":\\", "");
                    //директория сохранённого файла
                    string dir = Path.GetDirectoryName(path);
                    //имя сохранённого файла
                    string name = Path.GetFileName(path);
                    //имя сохранённого файла без расширения
                    string namex = Path.GetFileNameWithoutExtension(path);
                    //диск на котором программа
                    string prog = Path.GetPathRoot(Application.StartupPath);
                    //Путь к компилятору + имя файла
                    string compiler = $@"{sys2}:\Windows\Microsoft.NET\Framework\v3.5\csc.exe";
                    //запуск компилятора
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = $"{compiler}";
                        process.StartInfo.WorkingDirectory = dir;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardInput = true;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.Arguments = $@"/out:{namex}.exe  /nologo {path}";
                        process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                        process.Start();
                        string outs;//output для csc.exe
                        outs = process.StandardOutput.ReadToEnd().ToString();//запись output
                        richTextBox1.Text = outs;//вывод output'a
                        //ожидание окончания процесса
                        process.WaitForExit();
                    }
                }
                else
                {
                    SaveAs();
                }
            }
        }
        private void ВыделитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void СоздатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Page();
        }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void СохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void ЗакрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и удалять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (selectedRtb.IsChanged == true)//есть ли изменения которые можно сохранить
                {

                    DialogResult dialogResult = MessageBox.Show($"Сохранить изменения в {selT.Text}", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if ((File.Exists(selT.Tag.ToString())) || (File.Exists(selectedRtb.Tag.ToString())))//Проверка на существование путей
                        {                                                                                   //из сохранения или открытия(проверка на есть ли )
                            if (File.Exists(selT.Tag.ToString()))                                           //реальный файл или просто вкладка без ничего
                            {
                                Save();//если файл есть то сохранение либо в путь сохранения либо в путь откытия
                            }
                            else
                            {
                                Save();
                            }
                        }
                        else
                        {
                            SaveAs();//файла нет значит сохранить как
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //user нажал нет значит ничего делать не надо
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        return;//user отменил решение
                    }
                }
                metroTabControl1.Controls.Remove(selT);//удаление текущей страницы
            }
        }

        private void ОтменаДействияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //ctrl+z отмена
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Undo();
            }
        }


        private void ВосстановитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //ctrl+y вперёд
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Redo();
            }
        }

        private void КопироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //скопировать
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Copy();
            }
        }

        private void ВставкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //вставить 
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Paste();
            }
        }

        private void MetroTabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы (значит она есть)
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                сохранитьКакToolStripMenuItem.Enabled = true;//включение сохранить как(ведь страница есть)
                закрытьToolStripMenuItem.Enabled = true;//страница есть,а значит её можно закрыть
                скомпилироватьToolStripMenuItem.Enabled = true;//отключение кнопок
                выполнитьToolStripMenuItem1.Enabled = true;//отвечающих за
                скомпилироватьИВыполнитьToolStripMenuItem.Enabled = true;//компиляцию,запуск///и тд
                выделитьВсёToolStripMenuItem.Enabled = true;
                вВерхнийРегистрToolStripMenuItem.Enabled = true;
                вНижнийРегистрToolStripMenuItem.Enabled = true;
                вставитьубратьКомментарииToolStripMenuItem.Enabled = true;
                режимВставкиИЗаменыToolStripMenuItem.Enabled = true;
                убратьТекущуюСтрокуToolStripMenuItem.Enabled = true;
                найтиToolStripMenuItem.Enabled = true;
                заменитьToolStripMenuItem.Enabled = true;
                перейтиКСтрокеToolStripMenuItem.Enabled = true;
                приблизитьToolStripMenuItem.Enabled = true;
                отдалитьToolStripMenuItem.Enabled = true;
                добавитьЗакладкуToolStripMenuItem.Enabled = true;
                убратьЗакладкуToolStripMenuItem.Enabled = true;
                перейтиКЗакладкеToolStripMenuItem.Enabled = true;
                перейтиКЗакладкеToolStripMenuItem1.Enabled = true;
                toolStripCloseF.Enabled = true;
                toolStripFind.Enabled = true;
                toolStripZoomPlus.Enabled = true;
                toolStripZoomMinus.Enabled = true;
                toolStripCompRum.Enabled = true;
                if (selectedRtb.IsChanged == true)
                {
                    toolStripSaveF.Enabled = true;
                    сохранитьToolStripMenuItem.Enabled = true;
                }
                else
                {
                    toolStripSaveF.Enabled = false;
                    сохранитьToolStripMenuItem.Enabled = false;
                }
                if (selectedRtb.UndoEnabled == true)//блок,разблок кнопки в зав-ти от готовности undo(отмена)
                {
                    отменаДействияToolStripMenuItem.Enabled = true;
                }
                else
                {
                    отменаДействияToolStripMenuItem.Enabled = false;
                }
                if (selectedRtb.RedoEnabled == true)//блок,разблок кнопки в зав-ти от готовности redo(восстановление)
                {
                    восстановитьToolStripMenuItem.Enabled = true;
                }
                else
                {
                    восстановитьToolStripMenuItem.Enabled = false;
                }
                вставкаToolStripMenuItem.Enabled = true;//есть страница значит можно разблокировать кнопку
            }
        }

        private void MetroTabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            сохранитьКакToolStripMenuItem.Enabled = false;//при удалении страницы,выключается кнопка
            отменаДействияToolStripMenuItem.Enabled = false;//выкл undo,redo
            восстановитьToolStripMenuItem.Enabled = false;//
            закрытьToolStripMenuItem.Enabled = false;//страницы нет,значит её закрыть нельзя
            скомпилироватьToolStripMenuItem.Enabled = false;//отключение кнопок
            выполнитьToolStripMenuItem1.Enabled = false;//отвечающих за
            скомпилироватьИВыполнитьToolStripMenuItem.Enabled = false;//компиляцию,запуск///и тд
            копироватьToolStripMenuItem.Enabled = false;//блок кнопок копирования 
            вставкаToolStripMenuItem.Enabled = false;//и вставки
            вырезатьToolStripMenuItem.Enabled = false;/////
            выделитьВсёToolStripMenuItem.Enabled = false;
            вВерхнийРегистрToolStripMenuItem.Enabled = false;
            вНижнийРегистрToolStripMenuItem.Enabled = false;
            вставитьубратьКомментарииToolStripMenuItem.Enabled = false;
            режимВставкиИЗаменыToolStripMenuItem.Enabled = false;
            убратьТекущуюСтрокуToolStripMenuItem.Enabled = false;
            найтиToolStripMenuItem.Enabled = false;
            заменитьToolStripMenuItem.Enabled = false;
            перейтиКСтрокеToolStripMenuItem.Enabled = false;
            приблизитьToolStripMenuItem.Enabled = false;
            отдалитьToolStripMenuItem.Enabled = false;
            добавитьЗакладкуToolStripMenuItem.Enabled = false;
            убратьЗакладкуToolStripMenuItem.Enabled = false;
            перейтиКЗакладкеToolStripMenuItem.Enabled = false;
            перейтиКЗакладкеToolStripMenuItem1.Enabled = false;
            toolStripSaveF.Enabled = false;
            toolStripCloseF.Enabled = false;
            toolStripFind.Enabled = false;
            toolStripZoomPlus.Enabled = false;
            toolStripZoomMinus.Enabled = false;
            toolStripCompRum.Enabled = false;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и удалять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (selectedRtb.IsChanged == true)//есть ли изменения которые можно сохранить
                {
                    DialogResult dialogResult = MessageBox.Show($"Сохранить изменения в {selT.Text}", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if ((File.Exists(selT.Tag.ToString())) || (File.Exists(selectedRtb.Tag.ToString())))//Проверка на существование путей
                        {                                                                                   //из сохранения или открытия(проверка на есть ли )
                            if (File.Exists(selT.Tag.ToString()))                                           //реальный файл или просто вкладка без ничего
                            {
                                Save();//если файл есть то сохранение либо в путь сохранения либо в путь откытия
                            }
                            else
                            {
                                Save();
                            }
                        }
                        else
                        {
                            SaveAs();//файла нет значит сохранить как
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //user нажал нет значит ничего делать не надо
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        e.Cancel = true;//отмена закрытия формы
                    }

                }
            }
        }

        private void НайтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и удалять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.ShowFindDialog();//вызов диалога поиска
            }
        }

        private void ЗаменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и удалять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.ShowReplaceDialog();//вызов диалога поиска + замены
            }
        }

        private void ПерейтиКСтрокеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.ShowGoToDialog();//вызов диалога перейти к
            }
        }

        private void ВыделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.SelectAll();//вызов диалога выделить всё
            }
        }

        private void ПриблизитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Zoom = selectedRtb.Zoom + 10;
            }
        }

        private void ОтдалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Zoom = selectedRtb.Zoom - 10;
            }
        }

        private void ВВерхнийРегистрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.UpperCase();
            }
        }

        private void ВНижнийРегистрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.LowerCase();
            }
        }

        private void ВставитьубратьКомментарииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.CommentSelected();
            }
        }

        private void РежимВставкиИЗаменыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                SendKeys.Send("{Insert}");
            }
        }

        private void УбратьТекущуюСтрокуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.ClearCurrentLine();
            }
        }

        private void ВырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Cut();
                вырезатьToolStripMenuItem.Enabled = false;
            }
        }

        private void СкомпилироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compile();
        }

        private void ВыполнитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void СкомпилироватьИВыполнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompileWithRun();
        }

        private void СохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MetroContextMenu1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ЗакрытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ЗакрытьToolStripMenuItem_Click(sender,e);
        }

        private void RichTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(richTextBox1.Handle);//скрытие курсора
        }

        private void ДобавитьЗакладкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                //selectedRtb.UnbookmarkLine(Place.Empty.iLine);
                selectedRtb.BookmarkLine(selectedRtb.Selection.Start.iLine);
            }
        }

        private void УбратьЗакладкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                //selectedRtb.UnbookmarkLine(Place.Empty.iLine);
                selectedRtb.UnbookmarkLine(selectedRtb.Selection.Start.iLine);
            }
        }

        private void ПерейтиКЗакладкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //при переключении страницы на главную то если есть изменения кнопка не блокируется
            TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
            FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
            selectedRtb.GotoNextBookmark(selectedRtb.Selection.Start.iLine);
            
        }

        private void ПерейтиКЗакладкеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //при переключении страницы на главную то если есть изменения кнопка не блокируется
            TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
            FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
            selectedRtb.GotoPrevBookmark(selectedRtb.Selection.Start.iLine);
        }

        private void ToolStripCreateF_Click(object sender, EventArgs e)
        {
            создатьToolStripMenuItem.PerformClick();
        }

        private void ToolStripOpenF_Click(object sender, EventArgs e)
        {
            открытьToolStripMenuItem.PerformClick();
        }

        private void ToolStripSaveF_Click(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem.PerformClick();
        }

        private void ToolStripCloseF_Click(object sender, EventArgs e)
        {
            закрытьToolStripMenuItem.PerformClick();
        }

        private void ToolStripFind_Click(object sender, EventArgs e)
        {
            найтиToolStripMenuItem.PerformClick();
        }

        private void ToolStripZoomPlus_Click(object sender, EventArgs e)
        {
            приблизитьToolStripMenuItem.PerformClick();
        }

        private void ToolStripZoomMinus_Click(object sender, EventArgs e)
        {
            отдалитьToolStripMenuItem.PerformClick();
        }

        private void ToolStripCompRum_Click(object sender, EventArgs e)
        {
            скомпилироватьИВыполнитьToolStripMenuItem.PerformClick();
        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AboutProgramm About = new AboutProgramm();
            About.ShowDialog();
        }
    }
}
//1)ввести где надо проверки try catch для пущей безопаности
//возможность растягивать форму в право и лево,поработать над этим а то не оч растягивается
