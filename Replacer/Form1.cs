using System;
using System.IO;
using System.Windows.Forms;

namespace Replacer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new string[] {
                ReplaceWhere.Before.ToString(),
                ReplaceWhere.Replace.ToString(),
                ReplaceWhere.After.ToString()
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void ReadAndReplace(string word, string filePath, string fileName, ReplaceWhere where, bool overrideFile)
        {
            var fileText = "";

            using (var reader = new StreamReader(filePath + fileName))
                fileText = reader.ReadToEnd().Replace(word,
                    (where == ReplaceWhere.After ? word : "") +
                    ";" +
                    (where == ReplaceWhere.Before ? word : ""));

            using (var writer = new StreamWriter(filePath + (overrideFile ? "out" : "") + fileName))
                writer.Write(fileText);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                ReadAndReplace(
                    textBox1.Text,
                    openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.LastIndexOf('\\') + 1),
                    openFileDialog1.SafeFileName,
                    comboBox1.SelectedIndex == -1 ? (ReplaceWhere)2 : (ReplaceWhere)comboBox1.SelectedIndex,
                    checkBox1.Checked
                );
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public enum ReplaceWhere
    {
        Before,
        Replace,
        After
    }
}
