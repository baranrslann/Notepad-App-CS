using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string searchWord = "";
        int lastIndex = 0;
        string currentFilePath = "";
        public Form1()
        {
            InitializeComponent();
        }


       


        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void toolStripMenuItem36_Click(object sender, EventArgs e)
        {
            if (richTextBox1.ZoomFactor < 64)
            {
                richTextBox1.ZoomFactor += 0.1f;
            }
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            string aranan = Interaction.InputBox(
                "Search for text:",
                "Find",
                ""
                );

            if (string.IsNullOrEmpty(aranan))
            {
                return;
            }

            if (richTextBox1.Text.Contains(aranan))
            {
                int yer = richTextBox1.Text.IndexOf(aranan);

                richTextBox1.SelectionStart = yer;
                richTextBox1.SelectionLength = aranan.Length;
            }
            else
            {
                MessageBox.Show("The word you are looking for was not found.");

            }
        }



        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = saveFileDialog.FileName;
                System.IO.File.WriteAllText(currentFilePath, richTextBox1.Text);
            }
        }
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                toolStripMenuItem14_Click(sender, e);
            }
            else
            {
                System.IO.File.WriteAllText(currentFilePath, richTextBox1.Text);
            }
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            string goToLine = Interaction.InputBox(
                "Enter the line number you want to go to.",
                "Go"
                );

            int lineNumber = int.Parse(goToLine);

            int index = 0;
            int alreadyLine = 1;
            string text = richTextBox1.Text;

            for (int i = 0; i < text.Length; i++)
            {
                if (alreadyLine == lineNumber)
                    break;

                if (text[i] == '\n')
                {
                    alreadyLine++;
                    index = i + 1;
                }
            }

            richTextBox1.SelectionStart = index;
            richTextBox1.SelectionLength = 0;
            richTextBox1.Focus();

        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.Title = "Open File...";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                currentFilePath = openFileDialog.FileName;

                try
                {
                    string fileContent = System.IO.File.ReadAllText(currentFilePath);
                    richTextBox1.Text = fileContent;
                    this.Text = System.IO.Path.GetFileName(currentFilePath) + " - Notepad";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            if (lastIndex == 0)
            {
                lastIndex = richTextBox1.Text.IndexOf(searchWord);
            }
            else
            {
                lastIndex = richTextBox1.Text.IndexOf(searchWord, lastIndex + searchWord.Length);
            }


            if (lastIndex != -1)
            {
                richTextBox1.SelectionStart = lastIndex;
                richTextBox1.SelectionLength = searchWord.Length;
                richTextBox1.Focus();
            }
            else
            {
                MessageBox.Show("No next occurrence.");
            }
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            if (lastIndex > 0)
            {
                lastIndex = richTextBox1.Text.LastIndexOf(
                searchWord,
                lastIndex - 1
                );
                if (lastIndex != -1)
                {
                    richTextBox1.SelectionStart = lastIndex;
                    richTextBox1.SelectionLength = searchWord.Length;
                    richTextBox1.Focus();
                }
                else
                {
                    MessageBox.Show("No previous occurrence.");
                }
            }
            else
            {
                MessageBox.Show("Search index not found.");
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            string newWord = Interaction.InputBox(
                "Change"
                );

            richTextBox1.SelectedText = newWord;
            richTextBox1.Focus();

        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            string googleSearch = richTextBox1.SelectedText;

            if (googleSearch == "")
            {
                return;
            }
            string url = "https://www.google.com/search?q=" + googleSearch;
            System.Diagnostics.Process.Start(url);
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            string dateTime = DateTime.Now.ToString("HH:mm dd.MM.yyyy");


            richTextBox1.SelectedText = dateTime;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Form1 newWindow = new Form1();
            newWindow.Show();
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            pageSetupDialog.Document = printDocument1;
            pageSetupDialog.ShowDialog();
        }

        private void mainMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = richTextBox1.Font;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog.Font;
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = !richTextBox1.WordWrap;
            wordWrapToolStripMenuItem.Checked = richTextBox1.WordWrap;
        }

        private void fileSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top);
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.ZoomFactor > 0.5f)
            {
                richTextBox1.ZoomFactor -= 0.1f;
            }
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = !statusStrip1.Visible;
            statusBarToolStripMenuItem.Checked = statusStrip1.Visible;
        }

        private void restoreZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ZoomFactor = 1.0f;
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.bing.com/search?q=get+help+with+notepad+in+windows",
                UseShellExecute = true

            }
            );
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show
                ("Notepad v1.0\nDeveloped by [Baran ARSLAN]\nAll rights reserved 2026.",
                    "About Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/[baranrslann]/projenin-linki/issues",
                UseShellExecute = true
            });
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 1. ---- NEW WINDOW (Yeni Pencere) -> Ctrl + Shift + N ----
            if (keyData == (Keys.Control | Keys.Shift | Keys.N))
            {
                toolStripMenuItem7_Click(this, EventArgs.Empty);
                return true;
            }

            // 2. ---- SAVE AS... (Farklı Kaydet) -> Ctrl + Shift + S ----
            if (keyData == (Keys.Control | Keys.Shift | Keys.S))
            {
                toolStripMenuItem14_Click(this, EventArgs.Empty);
                return true;
            }

            // 3. ---- SAVE (Kaydet) -> Ctrl + S ----
            if (keyData == (Keys.Control | Keys.S))
            {
                toolStripMenuItem9_Click(this, EventArgs.Empty);
                return true;
            }

            // 4. ---- ZOOM IN (Ctrl + Numpad + veya Ctrl + Normal +) ----
            if (keyData == (Keys.Control | Keys.Add) || keyData == (Keys.Control | Keys.Oemplus))
            {
                toolStripMenuItem36_Click(this, EventArgs.Empty);
                return true;
            }

            // 5. ---- ZOOM OUT (Ctrl + Numpad - veya Ctrl + Normal -) ----
            if (keyData == (Keys.Control | Keys.Subtract) || keyData == (Keys.Control | Keys.OemMinus))
            {
                zoomOutToolStripMenuItem_Click(this, EventArgs.Empty);
                return true;
            }

            // 6. ---- RESTORE DEFAULT ZOOM (Ctrl + 0 veya Ctrl + Numpad 0) ----
            if (keyData == (Keys.Control | Keys.D0) || keyData == (Keys.Control | Keys.NumPad0))
            {
                restoreZoomToolStripMenuItem_Click(this, EventArgs.Empty);
                return true;
            }

            // 7. ---- PRINT (Yazdır) -> Ctrl + P ----
            if (keyData == (Keys.Control | Keys.P))
            {
                toolStripMenuItem12_Click(this, EventArgs.Empty);
                return true;
            }

            // 8. ---- TIME / DATE (Saat/Tarih) -> F5 ----
            if (keyData == Keys.F5)
            {
                toolStripMenuItem28_Click(this, EventArgs.Empty);
                return true;
            }

            if (keyData == (Keys.Control | Keys.O))
            {
                toolStripMenuItem8_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }   
}

