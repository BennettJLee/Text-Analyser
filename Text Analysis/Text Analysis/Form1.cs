using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Text_Analysis
{
    public partial class Form1 : Form
    {

        //Name: Ben Lee
        //Id: 1567783

        //For some reason the richTextBox doesnt load big text files, so some of the text files
        //won't load, so I tested with bacon1.txt

        List<string> WordsList = new List<string>();
        List<string> distinctList = new List<string>();

        const string FILTER = "TXT FILES|*.txt|All Files|*.*";

        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// initialises the word list
        /// </summary>
        private void Initialise()
        {
            WordsList.Clear();
        }
        /// <summary>
        /// This opens the text and adds elements to the wordsList
        /// and displays the total words and lines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string line = "";
            string[] csvArray;
            StreamReader reader;
            int lineCount = 0;
            int totalWords = 0;
            char[] delimiterChars = {' ', ',', '.', ':', '\t'};

            //Set the filter for the dialog control
            openFileDialog1.Filter = FILTER;
            //Check if the user selected a file to open
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //open the selected file
                reader = File.OpenText(openFileDialog1.FileName);

                Initialise();
                //repeat while it is not the end of file
                while (!reader.EndOfStream)
                {
                    //read a line from the file
                    line = reader.ReadLine();
                    richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);

                    //Split the values in the line using an array
                    csvArray = line.Split(delimiterChars);
                    csvArray = line.Split(null);
                    try
                    {
                        lineCount++;
                        totalWords += csvArray.Length;
                        
                        for(int i = 0; i < csvArray.Length; i++)
                        {
                            WordsList.Add(csvArray[i].ToLower());
                            
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error: " + line);
                    }
                }

                textBoxTotalLines.Text =  lineCount.ToString();
                textboxTotalWords.Text = totalWords.ToString();
                reader.Close();
                this.Text = openFileDialog1.FileName;
            }
        }
        /// <summary>
        /// This searches though the list and checks if they are the same as the search word
        /// if they are they will conuted and displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearchWord_Click(object sender, EventArgs e)
        {
            string searchWord = textBoxEnterWord.Text.ToLower();
            int index = WordsList.IndexOf(searchWord);
            int WordCount = 0;
            decimal WordPercentage = 0;
            //decimal TotalWords = WordsList.Count;
            decimal TotalWords = WordsList.Count;
            //For every element 
            for (int i = 0; i < WordsList.Count; i++)
            {
                //Add to total if the element equals the search word
                if (WordsList[i] == searchWord)
                {
                    WordCount++;
                }
            }
            textBoxWordAppears.Text = WordCount.ToString() + " times";
            WordPercentage = WordCount / TotalWords;
            MessageBox.Show("This word shows up " + WordPercentage.ToString("P") + " of the time");
        }
        /// <summary>
        /// This calculates the total distinct words and the richness of the text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            decimal totalWords = WordsList.Count;
            decimal distinctWords = distinctList.Count;
            decimal richness = 0;

            //Insert distinct elements from wordsList to distinctList 
            distinctList = WordsList.Distinct().ToList();
            //Calculate the richness of the text
            richness = distinctList.Count / totalWords;

            textBoxRichness.Text = distinctList.Count().ToString();
            MessageBox.Show("This text has a richness of " + richness.ToString("P"));
        }
    }
}