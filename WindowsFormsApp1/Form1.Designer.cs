namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитькакToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.закрытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отменадействияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.восстдействияToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.выполнитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.скомпилироватьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.выполнитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.скомпилироватьвыпToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.опрограммеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.создатьToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.открытьToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.сохранитьToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.закрытьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "cs";
            this.saveFileDialog1.Filter = "C# Source Code File (*.cs)|*.cs|Все файлы (*.*)|*.*";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "cs";
            this.openFileDialog1.Filter = "C# Source Code File (*.cs)|*.cs|Все файлы (*.*)|*.*";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.label1.Location = new System.Drawing.Point(478, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 6;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.AccessibleDescription = "";
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(809, 182);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);
            this.tabControl1.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.TabControl1_ControlRemoved);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem1,
            this.правкаToolStripMenuItem,
            this.выполнитьToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(809, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem1
            // 
            this.файлToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem2,
            this.открытьToolStripMenuItem2,
            this.toolStripSeparator,
            this.сохранитьToolStripMenuItem2,
            this.сохранитькакToolStripMenuItem2,
            this.toolStripSeparator2,
            this.закрытьToolStripMenuItem,
            this.toolStripSeparator1,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem1.Name = "файлToolStripMenuItem1";
            this.файлToolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem1.Text = "&Файл";
            // 
            // создатьToolStripMenuItem2
            // 
            this.создатьToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("создатьToolStripMenuItem2.Image")));
            this.создатьToolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.создатьToolStripMenuItem2.Name = "создатьToolStripMenuItem2";
            this.создатьToolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.создатьToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.создатьToolStripMenuItem2.Text = "&Создать";
            this.создатьToolStripMenuItem2.Click += new System.EventHandler(this.создатьToolStripMenuItem2_Click);
            // 
            // открытьToolStripMenuItem2
            // 
            this.открытьToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("открытьToolStripMenuItem2.Image")));
            this.открытьToolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.открытьToolStripMenuItem2.Name = "открытьToolStripMenuItem2";
            this.открытьToolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.открытьToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.открытьToolStripMenuItem2.Text = "&Открыть";
            this.открытьToolStripMenuItem2.Click += new System.EventHandler(this.открытьToolStripMenuItem2_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(177, 6);
            // 
            // сохранитьToolStripMenuItem2
            // 
            this.сохранитьToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("сохранитьToolStripMenuItem2.Image")));
            this.сохранитьToolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.сохранитьToolStripMenuItem2.Name = "сохранитьToolStripMenuItem2";
            this.сохранитьToolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.сохранитьToolStripMenuItem2.Text = "&Сохранить";
            this.сохранитьToolStripMenuItem2.Click += new System.EventHandler(this.сохранитьToolStripMenuItem2_Click);
            // 
            // сохранитькакToolStripMenuItem2
            // 
            this.сохранитькакToolStripMenuItem2.Name = "сохранитькакToolStripMenuItem2";
            this.сохранитькакToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.сохранитькакToolStripMenuItem2.Text = "Сохранить &как";
            this.сохранитькакToolStripMenuItem2.Click += new System.EventHandler(this.сохранитькакToolStripMenuItem2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // закрытьToolStripMenuItem
            // 
            this.закрытьToolStripMenuItem.Name = "закрытьToolStripMenuItem";
            this.закрытьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.закрытьToolStripMenuItem.Text = "Закрыть ";
            this.закрытьToolStripMenuItem.Click += new System.EventHandler(this.закрытьToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.выходToolStripMenuItem.Text = "Вы&ход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отменадействияToolStripMenuItem,
            this.восстдействияToolStripMenuItem1,
            this.toolStripSeparator3,
            this.копироватьToolStripMenuItem,
            this.вставкаToolStripMenuItem,
            this.toolStripSeparator4});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.правкаToolStripMenuItem.Text = "&Правка";
            // 
            // отменадействияToolStripMenuItem
            // 
            this.отменадействияToolStripMenuItem.Name = "отменадействияToolStripMenuItem";
            this.отменадействияToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.отменадействияToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.отменадействияToolStripMenuItem.Text = "&Отмена действия";
            this.отменадействияToolStripMenuItem.Click += new System.EventHandler(this.отменадействияToolStripMenuItem_Click);
            // 
            // восстдействияToolStripMenuItem1
            // 
            this.восстдействияToolStripMenuItem1.Name = "восстдействияToolStripMenuItem1";
            this.восстдействияToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.восстдействияToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.восстдействияToolStripMenuItem1.Text = "&Восстановить";
            this.восстдействияToolStripMenuItem1.Click += new System.EventHandler(this.отменадействияToolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(206, 6);
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("копироватьToolStripMenuItem.Image")));
            this.копироватьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.копироватьToolStripMenuItem.Text = "&Копировать";
            this.копироватьToolStripMenuItem.Click += new System.EventHandler(this.копироватьToolStripMenuItem_Click);
            // 
            // вставкаToolStripMenuItem
            // 
            this.вставкаToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("вставкаToolStripMenuItem.Image")));
            this.вставкаToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.вставкаToolStripMenuItem.Name = "вставкаToolStripMenuItem";
            this.вставкаToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.вставкаToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.вставкаToolStripMenuItem.Text = "Вст&авка";
            this.вставкаToolStripMenuItem.Click += new System.EventHandler(this.вставкаToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(206, 6);
            // 
            // выполнитьToolStripMenuItem
            // 
            this.выполнитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.скомпилироватьToolStripMenuItem1,
            this.выполнитьToolStripMenuItem1,
            this.скомпилироватьвыпToolStripMenuItem});
            this.выполнитьToolStripMenuItem.Name = "выполнитьToolStripMenuItem";
            this.выполнитьToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.выполнитьToolStripMenuItem.Text = "Выполнить";
            this.выполнитьToolStripMenuItem.Click += new System.EventHandler(this.выполнитьToolStripMenuItem_Click);
            // 
            // скомпилироватьToolStripMenuItem1
            // 
            this.скомпилироватьToolStripMenuItem1.Name = "скомпилироватьToolStripMenuItem1";
            this.скомпилироватьToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.скомпилироватьToolStripMenuItem1.Size = new System.Drawing.Size(262, 22);
            this.скомпилироватьToolStripMenuItem1.Text = "Скомпилировать";
            this.скомпилироватьToolStripMenuItem1.Click += new System.EventHandler(this.скомпилироватьToolStripMenuItem1_Click);
            // 
            // выполнитьToolStripMenuItem1
            // 
            this.выполнитьToolStripMenuItem1.Name = "выполнитьToolStripMenuItem1";
            this.выполнитьToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.выполнитьToolStripMenuItem1.Size = new System.Drawing.Size(262, 22);
            this.выполнитьToolStripMenuItem1.Text = "Выполнить";
            this.выполнитьToolStripMenuItem1.Click += new System.EventHandler(this.выполнитьToolStripMenuItem1_Click);
            // 
            // скомпилироватьвыпToolStripMenuItem
            // 
            this.скомпилироватьвыпToolStripMenuItem.Name = "скомпилироватьвыпToolStripMenuItem";
            this.скомпилироватьвыпToolStripMenuItem.ShortcutKeyDisplayString = "F5";
            this.скомпилироватьвыпToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.скомпилироватьвыпToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.скомпилироватьвыпToolStripMenuItem.Text = "Скомпилировать и выполнить";
            this.скомпилироватьвыпToolStripMenuItem.Click += new System.EventHandler(this.скомпилироватьToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.опрограммеToolStripMenuItem1});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Спра&вка";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(155, 6);
            // 
            // опрограммеToolStripMenuItem1
            // 
            this.опрограммеToolStripMenuItem1.Name = "опрограммеToolStripMenuItem1";
            this.опрограммеToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.опрограммеToolStripMenuItem1.Text = "&О программе...";
            this.опрограммеToolStripMenuItem1.Click += new System.EventHandler(this.опрограммеToolStripMenuItem1_Click);
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.создатьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.создатьToolStripMenuItem.Text = "Создать";
            this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripButton,
            this.открытьToolStripButton,
            this.сохранитьToolStripButton,
            this.toolStripSeparator7,
            this.toolStripButton4,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(809, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // создатьToolStripButton
            // 
            this.создатьToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.создатьToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("создатьToolStripButton.Image")));
            this.создатьToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.создатьToolStripButton.Name = "создатьToolStripButton";
            this.создатьToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.создатьToolStripButton.Text = "&Создать";
            this.создатьToolStripButton.Click += new System.EventHandler(this.создатьToolStripButton_Click);
            // 
            // открытьToolStripButton
            // 
            this.открытьToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.открытьToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("открытьToolStripButton.Image")));
            this.открытьToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.открытьToolStripButton.Name = "открытьToolStripButton";
            this.открытьToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.открытьToolStripButton.Text = "&Открыть";
            this.открытьToolStripButton.Click += new System.EventHandler(this.открытьToolStripButton_Click);
            // 
            // сохранитьToolStripButton
            // 
            this.сохранитьToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.сохранитьToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("сохранитьToolStripButton.Image")));
            this.сохранитьToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.сохранитьToolStripButton.Name = "сохранитьToolStripButton";
            this.сохранитьToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.сохранитьToolStripButton.Text = "&Сохранить";
            this.сохранитьToolStripButton.Click += new System.EventHandler(this.сохранитьToolStripButton_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Скомпилировать и выполнить";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "RemoveTable";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 231);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 123);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(809, 182);
            this.panel2.TabIndex = 11;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.закрытьToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(121, 26);
            // 
            // закрытьToolStripMenuItem1
            // 
            this.закрытьToolStripMenuItem1.Name = "закрытьToolStripMenuItem1";
            this.закрытьToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.закрытьToolStripMenuItem1.Text = "Закрыть";
            this.закрытьToolStripMenuItem1.Click += new System.EventHandler(this.закрытьToolStripMenuItem1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ShortcutsEnabled = false;
            this.richTextBox1.Size = new System.Drawing.Size(809, 123);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 354);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Программа-эмулятор среды объектно-ориентированного программирования";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem выполнитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem скомпилироватьвыпToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem сохранитькакToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отменадействияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem восстдействияToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem опрограммеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton создатьToolStripButton;
        private System.Windows.Forms.ToolStripButton открытьToolStripButton;
        private System.Windows.Forms.ToolStripButton сохранитьToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem скомпилироватьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem выполнитьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem закрытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem закрытьToolStripMenuItem1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

