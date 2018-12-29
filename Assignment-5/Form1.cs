/*******************************************************************************************************************
 *                                                                                                                 *
 *  CSCI 473/504							Assignment 5 								 Fall 2018                 *                                           
 *																										           *
 *  Programmer's: Sandeep Alla (z1821331) *  
 *																										           *
 *  Date Due  : November 15, 2018			File :	Form1.cs     					     				           *                          
 *																										           *
 *  Purpose   : A Form application for Suduko Puzzle game													       *
 *																							                       *
 ******************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Assignment_5
{
	public partial class Form1 : Form
	{
		//Declaring all the local variables 
		[DllImport("user32.dll")] // Dll for HideCaret function
		static extern bool HideCaret(IntPtr hWnd);
		TextBox tb = null;
		List<DirectoryData> fileLocations = new List<DirectoryData>();
		List<string> sudokuQuestion = new List<string>();
		List<string> sudokuAnswer = new List<string>();
		List<string> sudokuEditedList = new List<string>();
		List<string> wrongTBs = new List<string>();
		int[,] sudokuQuestioninArray = new int[9, 9];
		int[,] sudokuAnswerinArray = new int[9, 9];
		int[,] sudokuEdited = new int[9, 9];
		string level = null;		
		private bool _timerRunning = false;		
		TimeSpan _totalElapsedTime = TimeSpan.Zero;
		string currentFileName = "";
		string currentFileType = "";
		TimeSpan fastestTimeTakenEasy = TimeSpan.Zero;
		TimeSpan fastestTimeTakenMedium = TimeSpan.Zero;
		TimeSpan fastestTimeTakenHard = TimeSpan.Zero;
		TimeSpan avgTimeTakenEasy = TimeSpan.Zero;
		TimeSpan avgTimeTakenHard = TimeSpan.Zero;
		TimeSpan avgTimeTakenMedium = TimeSpan.Zero;
		TimeSpan timeFromFile = TimeSpan.Zero;
		bool saved = false;
		bool loaded = false;
		bool edited = false;
		bool newGame = false;
		bool isHelp = false;
		bool isReset = false;
		bool alreadySaved = false;
		bool isProgress = false;
		bool isCompleted = false;		
		Stopwatch timer = new Stopwatch(); // Stopwatch to record the time 

		public Form1()
		{
			InitializeComponent();			
		}

		//Method to read all the available puzzle locations from directory.txt
		public void readFileLocations()
		{
			//If the File exists
			if (File.Exists(Directory.GetCurrentDirectory() + "\\directory.txt"))
			{
				//Reading the file and storing all the values as objects of directoryData class into a list
				using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\directory.txt")) // StreamReader is used to read data from a text file
				{
					fileLocations = new List<DirectoryData>();
					string type;
					string fileName;
					string status;
					string timeElapsed;
					string data = inFile.ReadLine(); // Reading the firstline and storing it in string variable data
					while (data != null) // Loop until there is nothing to read from the file
					{
						string[] result = data.Split('/'); //Splitting the data seperated by ','
						type = result[0];
						fileName = result[1];
						status = result[2];
						timeElapsed = result[3];
						DirectoryData DD = new DirectoryData(type, fileName, status, timeElapsed);
						fileLocations.Add(DD);
						data = inFile.ReadLine();
					}

					inFile.Close(); // Closing the file
				}
			}

			//Reading the elapsed times of different games in a Easy difficulty and calculating the averages each and every time
			if (File.Exists(Directory.GetCurrentDirectory() + "\\AvgTrackingEasy.txt"))
			{
				//Reading the file
				using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\AvgTrackingEasy.txt")) // StreamReader is used to read data from a text file
				{
					List<TimeSpan> easyTimespans = new List<TimeSpan>();
					string data = inFile.ReadLine(); // Reading the firstline and storing it in string variable data
					while (data != "" && data != null) // Loop until there is nothing to read from the file
					{
						TimeSpan tt = TimeSpan.Parse(data);
						easyTimespans.Add(tt);
						data = inFile.ReadLine();
					}

					if (easyTimespans.Any())
					{
						double doubleAverageTicks = easyTimespans.Average(timeSpan => timeSpan.Ticks);
						long longAverageTicks = Convert.ToInt64(doubleAverageTicks);
						avgTimeTakenEasy = new TimeSpan(longAverageTicks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond);
					}
					inFile.Close(); // Closing the file
				}
			}

			//Reading the elapsed times of different games in a Medium difficulty and calculating the averages each and every time
			if (File.Exists(Directory.GetCurrentDirectory() + "\\AvgTrackingMedium.txt"))
			{
				//Reading the file
				using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\AvgTrackingMedium.txt")) // StreamReader is used to read data from a text file
				{
					List<TimeSpan> mediumTimespans = new List<TimeSpan>();
					string data = inFile.ReadLine(); // Reading the firstline and storing it in string variable data
					while (data != "" && data != null) // Loop until there is nothing to read from the file
					{
						TimeSpan tt = TimeSpan.Parse(data);
						mediumTimespans.Add(tt);
						data = inFile.ReadLine();
					}

					if (mediumTimespans.Any())
					{
						double doubleAverageTicks = mediumTimespans.Average(timeSpan => timeSpan.Ticks);
						long longAverageTicks = Convert.ToInt64(doubleAverageTicks);
						avgTimeTakenMedium = new TimeSpan(longAverageTicks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond);
					}
					inFile.Close(); // Closing the file
				}
			}

			//Reading the elapsed times of different games in a Hard difficulty and calculating the averages each and every time
			if (File.Exists(Directory.GetCurrentDirectory() + "\\AvgTrackingHard.txt"))
			{
				//Reading the file
				using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\AvgTrackingHard.txt")) // StreamReader is used to read data from a text file
				{
					List<TimeSpan> hardTimespans = new List<TimeSpan>();
					string data = inFile.ReadLine(); // Reading the firstline and storing it in string variable data
					while (data != "" && data != null) // Loop until there is nothing to read from the file
					{
						TimeSpan tt = TimeSpan.Parse(data);
						hardTimespans.Add(tt);
						data = inFile.ReadLine();
					}

					if (hardTimespans.Any())
					{
						double doubleAverageTicks = hardTimespans.Average(timeSpan => timeSpan.Ticks);
						long longAverageTicks = Convert.ToInt64(doubleAverageTicks);
						avgTimeTakenHard = new TimeSpan(longAverageTicks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond);
					}
					inFile.Close(); // Closing the file
				}
			}

			//Reading the fastest time recorded for easy difficulty
			if (File.Exists(Directory.GetCurrentDirectory() + "\\fastTrackingEasy.txt"))
			{
				//Reading the file
				using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\fastTrackingEasy.txt")) // StreamReader is used to read data from a text file
				{
					string data = inFile.ReadLine(); // Reading the firstline and storing it in string variable data
					while(data != "" && data != null) // Loop until there is nothing to read from the file
					{
						fastestTimeTakenEasy = TimeSpan.Parse(data);
						data = inFile.ReadLine();
					}

					inFile.Close(); // Closing the file
				}
			}

			//Reading the fastest time recorded for Medium difficulty
			if (File.Exists(Directory.GetCurrentDirectory() + "\\fastTrackingMedium.txt"))
			{
				//Reading the file
				using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\fastTrackingMedium.txt")) // StreamReader is used to read data from a text file
				{
					string data = inFile.ReadLine(); // Reading the firstline and storing it in string variable data
					while (data != "" && data != null) // Loop until there is nothing to read from the file
					{
						fastestTimeTakenMedium = TimeSpan.Parse(data);
						data = inFile.ReadLine();
					}

					inFile.Close(); // Closing the file
				}
			}

			//Reading the fastest time recorded for Hard difficulty
			if (File.Exists(Directory.GetCurrentDirectory() + "\\fastTrackingHard.txt"))
			{
				using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\fastTrackingHard.txt")) // StreamReader is used to read data from a text file
				{
					string data = inFile.ReadLine(); // Reading the firstline and storing it in string variable data
					while (data != "" && data != null) // Loop until there is nothing to read from the file
					{
						fastestTimeTakenHard = TimeSpan.Parse(data);
						data = inFile.ReadLine();
					}

					inFile.Close(); // Closing the file
				}
			}
			

		}

		//Method to read the data of a particular puzzle from the files
		public void readFileData(string level)
		{
			sudokuQuestion = new List<string>();
			sudokuAnswer = new List<string>();
			sudokuEditedList = new List<string>();
			readFileLocations();
			bool isAnswer = false;
			bool isEdited = false;
			//Looping through all the available file locations
			foreach(var x in fileLocations)
			{
				//Reading the puzzle which equals the level selected by and user and the first unsolved puzzle
				if (x.Type == level && (x.Status == "unsolved" || x.Status == "saved"))
				{
					if(x.Status == "saved")
					{
						alreadySaved = true;							 
					}

					currentFileName = x.FileName;
					currentFileType = x.Type;
					timeFromFile = TimeSpan.Parse(x.TimeElapsed);
					//Reading the file
					using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\" + x.Type + "\\" + x.FileName)) // StreamReader is used to read data from a text file
					{
						string data = inFile.ReadLine(); // Reading the firstline and storing it in string variable data
						while (data != null) // Loop until there is nothing to read from the file
						{
							if(data != "" && isAnswer != true && isEdited != true )
							{
								sudokuQuestion.Add(data);
							}

							if (data == "")
							{
								isAnswer = true;								
							}

							if(data == "LastEditedData")
							{
								isAnswer = false;
								isEdited = true;
							}

							if(data != "" && isEdited == true && isAnswer != true)
							{
								if(data != "LastEditedData")
									sudokuEditedList.Add(data);
							}

							if (data != "" && isAnswer == true && isEdited != true)
							{
								sudokuAnswer.Add(data);
							}

							data = inFile.ReadLine();
						}

						inFile.Close(); // Closing the file
					}

					break;
				}
			}

			//Initialiing all the 2d arrays
			sudokuQuestioninArray = new int[9, 9];
			sudokuEdited = new int[9, 9];
			sudokuAnswerinArray = new int[9, 9];

			//Storing the Puzzle Question in SudukoQuestioninArray 
			for (int i = 0; i < sudokuQuestion.Count; i++)
			{
				int[] splitData = new int[9];
				string temp = sudokuQuestion[i];

				for ( int k = 0; k < temp.Length; k++)
				{					
					splitData[k] = Convert.ToInt32(temp[k]);
				}

				for ( int j = 0; j < splitData.Length; j++)
				{
					sudokuQuestioninArray[i, j] = splitData[j] - 48;
				}
			}

			//Storing the Edited data of the user in SudukoEdited Array
			if (sudokuEditedList.Count > 0)
			{
				for (int i = 0; i < sudokuEditedList.Count; i++)
				{
					int[] splitData = new int[9];
					string temp = sudokuEditedList[i];

					for (int k = 0; k < temp.Length; k++)
					{
						splitData[k] = Convert.ToInt32(temp[k]);
					}

					for (int j = 0; j < splitData.Length; j++)
					{
						sudokuEdited[i, j] = splitData[j] - 48;
					}
				}
			}
			else
			{
				sudokuEdited = sudokuQuestioninArray;
			}

			//Storing the Puzzle Answer in SudukoAnswer Array
			for (int i = 0; i < sudokuAnswer.Count; i++)
			{
				int[] splitData = new int[9];
				string temp = sudokuAnswer[i];

				for (int k = 0; k < temp.Length; k++)
				{
					splitData[k] = Convert.ToInt32(temp[k]);
				}

				for (int j = 0; j < splitData.Length; j++)
				{
					sudokuAnswerinArray[i, j] = splitData[j] - 48;
				}
			}
		}


		//Method to draw the 81 textboxes dynamically onto a panel
		private void DrawGrid()
		{
			panel.Controls.Clear();
			const int lines = 9;
			int x = 0;
			int y = 0;
			int xSpacing = panel.Width / 9;
			int ySpacing = panel.Height / 9;

			if(!isHelp && !isProgress)
				readFileData(level);

			//Iterating through each row and each column with 9 textboxs for a row
			for (int r = 0; r < lines; r++)
			{
				for (int c = 0; c < lines; c++)
				{
					//Creating a new textbox
					tb = new TextBox();
					//Assigning a name to the textbox, location, font,textAlign and width, height
					tb.Name = "box-" + r.ToString() + "-" + c.ToString();
					tb.AutoSize = false;
					tb.Location = new Point(x, y);
					tb.Font = new Font(tb.Font.FontFamily, 15);
					tb.TextAlign = HorizontalAlignment.Center;
					tb.Width = 40;
					tb.Height = 30;
					
					//Based upon several conditions i will open a new puzzle or the saved instance.
					if ((loaded != true && alreadySaved != true && isHelp != true && isProgress != true) || isReset )
					{
						if (sudokuQuestioninArray[r, c] != 0)
						{
							tb.Text = sudokuQuestioninArray[r, c].ToString();
							tb.Enabled = false;
							tb.Font = new Font(tb.Font, FontStyle.Bold);							
							tb.BorderStyle = BorderStyle.Fixed3D;
							newGame = true;
						}
						else
						{
							tb.Text = "";
						}						
					}
					else
					{
						if (sudokuEdited[r, c] != 0)
						{
							tb.Text = sudokuEdited[r, c].ToString();
							tb.Enabled = true;							
							tb.BorderStyle = BorderStyle.Fixed3D;
							edited = true;
						}
						else
						{
							tb.Text = "";
						}

						if(sudokuQuestioninArray[r,c] != 0)
						{
							tb.Enabled = false;
							tb.Font = new Font(tb.Font, FontStyle.Bold);							
						}
					}

					//If the progress flag is true
					if (isProgress)
					{
						//Looping through all the wrongly inputed textboxes and making them red
						foreach (var cells in wrongTBs)
						{
							string[] result = cells.Split('-');
							if (Convert.ToInt32(result[0]) == r && Convert.ToInt32(result[1]) == c)
							{
								tb.Enabled = true;
								tb.BackColor = Color.Red;
							}
						}
					}

					//Events for the textboxes
					tb.KeyPress += new KeyPressEventHandler(tb_keyPress);
					tb.TextChanged += new EventHandler(tb_textChanged);
					tb.MouseDown += new MouseEventHandler(tb_mouseDown);
					tb.Leave += new EventHandler(tb_mouseLeave);					
					//Adding the textbox to the panel
					this.panel.Controls.Add(tb);
					x += xSpacing;
				}
				y += ySpacing;
				x = 0;
			}

			isReset = false;
			isHelp = false;			
			alreadySaved = false;
			isProgress = false;

		}		

		//On mousedown on the textbox change the color and hide the blinking cursor
		private void tb_mouseDown(object sender, MouseEventArgs e)
		{
			var tb = sender as TextBox;
			HideCaret(tb.Handle);		
			tb.BackColor = Color.LightBlue;				
		}

		//On mouseleave from the textbox make the color to white
		private void tb_mouseLeave(object sender, EventArgs e)
		{
			var tb = sender as TextBox;
			tb.BackColor = Color.White;
		}		

		//On Keypress on the textbox
		private void tb_keyPress(object sender, KeyPressEventArgs e)
		{
			SaveBtn.Enabled = true;
			var tb = sender as TextBox;
			tb.MaxLength = 1;
			//Allowing only numeric digits except 0 and backspace
			if ((Char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back) &&  e.KeyChar.ToString() != "0")
			{				
				e.Handled = false;								
			}
			else 
			{
				e.Handled = true;
			}
		}

		//On textchanged in a textbox
		private void tb_textChanged(object sender, EventArgs e)
		{
			var tb = sender as TextBox;
			//MessageBox.Show(tb.Text);
			
			//updating the value of the textbox to the user entered value and also storing that value at that particular textbox row and column in to the sudukoedited array
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					string tbName = "box-" + i + "-" + j;
					if (tb.Name == tbName && tb.Text !=  "")
					{
						sudokuEdited[i, j] = Convert.ToInt32(tb.Text);
						tb.BorderStyle = BorderStyle.Fixed3D;											
					}
					else if(tb.Name == tbName && tb.Text == "")
					{
						sudokuEdited[i, j] = 0;						
					}
				}
			}

			int count = 0;
			//For counting the number of empty textboxes
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					if (sudokuEdited[i,j] == sudokuAnswerinArray[i,j])
					{
						count++;			
					}
				}
			}

			string ftt = "";
			string att = "";

			if (count == 81)
			{
				timer.Stop();
				//Creating the following files if they doesn't exists
				if (!File.Exists(Environment.CurrentDirectory + "\\AvgTrackingEasy.txt"))
				{
					File.Create(Environment.CurrentDirectory + "\\AvgTrackingEasy.txt").Dispose();					
				}

				if (!File.Exists(System.Environment.CurrentDirectory + "\\AvgTrackingMedium.txt"))
				{
					File.Create(Environment.CurrentDirectory + "\\AvgTrackingMedium.txt").Dispose();
				}

				if (!File.Exists(System.Environment.CurrentDirectory + "\\AvgTrackingHard.txt"))
				{
					File.Create(Environment.CurrentDirectory + "\\AvgTrackingHard.txt").Dispose();
				}

				StreamWriter trackEasyWrite = new StreamWriter(Environment.CurrentDirectory + "\\fastTrackingEasy.txt");
				StreamWriter trackMediumWrite = new StreamWriter(Environment.CurrentDirectory + "\\fastTrackingMedium.txt");
				StreamWriter trackHardWrite = new StreamWriter(Environment.CurrentDirectory + "\\fastTrackingHard.txt");

				_totalElapsedTime = new TimeSpan(timer.Elapsed.Hours, timer.Elapsed.Minutes, timer.Elapsed.Seconds);
				_totalElapsedTime = _totalElapsedTime + timeFromFile;
				//If the level is easy
				if (level == "easy")
				{
					//If the fastest time is zero assigning the first time
					if(fastestTimeTakenEasy == TimeSpan.Zero)
					{
						fastestTimeTakenEasy = _totalElapsedTime;
					}
					//Determining the fastest time based on the user elapsed time
					if(_totalElapsedTime < fastestTimeTakenEasy)
					{
						fastestTimeTakenEasy = _totalElapsedTime;
					}

					ftt = fastestTimeTakenEasy.ToString();
					att = avgTimeTakenEasy.ToString();
					//Writing the fastest time to a file
					trackEasyWrite.WriteLine(ftt);
					trackEasyWrite.Flush(); //write stream to file			
					//Writing the average time to a file
					File.AppendAllText(Environment.CurrentDirectory + "\\AvgTrackingEasy.txt", _totalElapsedTime + Environment.NewLine);
				}

				if (level == "medium")
				{
					//If the fastest time is zero assigning the first time
					if (fastestTimeTakenMedium == TimeSpan.Zero)
					{
						fastestTimeTakenMedium = _totalElapsedTime;
					}
					//Determining the fastest time based on the user elapsed time
					if (_totalElapsedTime < fastestTimeTakenMedium)
					{
						fastestTimeTakenMedium = _totalElapsedTime;
					}

					ftt = fastestTimeTakenMedium.ToString();
					att = avgTimeTakenMedium.ToString();
					//Writing the fastest time to a file
					trackMediumWrite.WriteLine(ftt);
					trackMediumWrite.Flush(); //write stream to file
					//Writing the average time to a file
					File.AppendAllText(Environment.CurrentDirectory + "\\AvgTrackingMedium.txt", _totalElapsedTime + Environment.NewLine);
				}

				if (level == "hard")
				{
					//If the fastest time is zero assigning the first time
					if (fastestTimeTakenHard == TimeSpan.Zero)
					{
						fastestTimeTakenHard = _totalElapsedTime;
					}
					//Determining the fastest time based on the user elapsed time
					if (_totalElapsedTime < fastestTimeTakenHard)
					{
						fastestTimeTakenHard = _totalElapsedTime;
					}

					ftt = fastestTimeTakenHard.ToString();
					att = avgTimeTakenHard.ToString();
					//Writing the fastest time to a file
					trackHardWrite.WriteLine(ftt);
					trackHardWrite.Flush(); //write stream to file	
					//Writing the average time to a file
					File.AppendAllText(Environment.CurrentDirectory + "\\AvgTrackingHard.txt", _totalElapsedTime + Environment.NewLine);
				}

				trackEasyWrite.Close(); //close the stream and reclaim memory
				trackMediumWrite.Close(); //close the stream and reclaim memory		
				trackHardWrite.Close(); //close the stream and reclaim memory

				isCompleted = true;
				StringBuilder newFile = new StringBuilder();
				string temp1 = "";
				string temp2 = "";
				string[] file = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\directory.txt");
				string elapsedTime = _totalElapsedTime.ToString();
				//Changing the status of the puzzle depending upon unsolved,saved and completed
				foreach (string line in file)
				{
					string[] result = line.Split('/');	
					//If the puzzle status is unsolved replace that, if it is saved replace that
					if (line.Contains(currentFileName) && line.Contains(currentFileType) && line.Contains("unsolved"))
					{
						temp1 = line.Replace("unsolved", "completed");
						temp2 = temp1.Replace(result[3], elapsedTime);
						newFile.Append(temp2 + "\r\n");
						continue;
					}
					else if (line.Contains(currentFileName) && line.Contains(currentFileType) && line.Contains("saved"))
					{
						temp1 = line.Replace("saved", "completed");
						temp2 = temp1.Replace(result[3], elapsedTime);
						newFile.Append(temp2 + "\r\n");
						continue;
					}
					newFile.Append(line + "\r\n");
				}
				File.WriteAllText(Directory.GetCurrentDirectory() + "\\directory.txt", newFile.ToString());
				
				readFileLocations();
				//Assigning the level for the next puzzle
				foreach (var x in fileLocations)
				{
					if (x.Status == "unsolved")
					{
						level = x.Type;
						if (level == "easy")
							selectLevelCB.Text = "Easy";
						else if (level == "Medium")
							selectLevelCB.Text = "medium";
						else if (level == "Hard")
							selectLevelCB.Text = "hard";
						
						break;
					}
				}

				//Getting the user input, if yes start new game else quit game
				var userOption = MessageBox.Show("Congratulations!You Solved the Puzzle" +
							  "\n**********Game Statistics**********" +
							  "\nTime taken to solve               : " + _totalElapsedTime + 
							  "\nFastest Time taken to solve       : " + ftt + 
							  "\nAverage Time taken to solve       : " + att +
							  "\n\n Do you want continue to next game(Yes/No):", "Solved" , MessageBoxButtons.YesNo);

				//Based the user selection
				switch (userOption)
				{
					case DialogResult.Yes:   // Yes button pressed
						DrawGrid();
						_totalElapsedTime = TimeSpan.Zero;
						timer.Reset();
						timer.Start();
						break;
					case DialogResult.No:    // No button pressed
						Application.Exit();						
						break;
					default:                 // Neither Yes nor No pressed (just in case)		
						Application.Exit();
						break;
				}			

			}

		}

		//Click Method for the newgame button
		private void newGameBtn_Click(object sender, EventArgs e)
		{
			// If the timer isn't already running
			if (!_timerRunning)
			{				
				 timer.Start();
				_timerRunning = true;
			}
			else // If the timer is already running
			{
				timer.Reset();
				timer.Start();
				_timerRunning = true;
			}
			pauseBtn.Text = "Pause";
			pauseBtn.Enabled = true;
			helpBtn.Enabled = true;
			resetBtn.Enabled = true;
			progressBtn.Enabled = true;
			DrawGrid();
		}

		//Method for the level dropdown box
		private void selectLevelCB_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				//Getting the current selected value from the combo box
				string selected = selectLevelCB.SelectedItem.ToString();
				if(selected == "Easy")
					level = "easy";
				else if (selected == "Medium")
					level = "medium";
				else if (selected == "Hard")
					level = "hard";
			}
			catch (Exception)
			{

			}
		}

		//Click method for the load button
		private void loadBtn_Click(object sender, EventArgs e)
		{
			timer.Reset();
			timer.Start();
			loaded = true;
			bool found = false;
			//Reading throught the directory and searching for the saved puzzle to load the edited data
			using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\directory.txt")) // StreamReader is used to read data from a text file
			{
				string data = inFile.ReadLine();
				while(data != null)
				{ 
					if (data.Contains("saved"))
					{
						string[] result = data.Split('/');
						string type = result[0];
						string fileName = result[1];
						currentFileType = type;
						level = type;
						currentFileName = result[1];
						found = true;
						break;
					}

					data = inFile.ReadLine();
				}
			}
			//If the saved file is found drawing the grid 
			if (found == true)
				DrawGrid();
			else
				MessageBox.Show("There is no saved game!");

			pauseBtn.Enabled = true;
			helpBtn.Enabled = true;
			resetBtn.Enabled = true;
			progressBtn.Enabled = true;
		}

		//Click method for the save button
		private void SaveBtn_Click(object sender, EventArgs e)
		{
			// When user clicks save changing the status of the puzzle from unsolved to saved in the directory.txt
			StringBuilder newFile = new StringBuilder();
			string temp1 = "";
			string temp2 = "";
			string[] file = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\directory.txt");
			_totalElapsedTime = new TimeSpan(timer.Elapsed.Hours, timer.Elapsed.Minutes, timer.Elapsed.Seconds);
			_totalElapsedTime = _totalElapsedTime + timeFromFile;
			string elapsedTime = _totalElapsedTime.ToString();
			foreach (string line in file)
			{
				string[] result = line.Split('/');				
				if (line.Contains(currentFileName) && line.Contains(currentFileType) && line.Contains("unsolved"))
				{					
					temp1 = line.Replace("unsolved", "saved");					
					temp2 = temp1.Replace(result[3], elapsedTime);
					newFile.Append(temp2 + "\r\n");					
					continue;
				}
				else if (line.Contains(currentFileName) && line.Contains(currentFileType) && line.Contains("saved"))
				{
					temp2 = line.Replace(result[3], elapsedTime);
					newFile.Append(temp2 + "\r\n");
					continue;
				}
				newFile.Append(line + "\r\n");
			}

			File.WriteAllText(Directory.GetCurrentDirectory() + "\\directory.txt", newFile.ToString());



			List<string> sudokuFileData = new List<string>();

			foreach (var x in sudokuQuestion)
			{
				sudokuFileData.Add(x);
			}

			sudokuFileData.Add("\n");

			foreach (var x in sudokuAnswer)
			{
				sudokuFileData.Add(x);
			}

			//Erasing all the currently present data in the puzzle data file and replacing it with the Question,Answer and the user edited data
			File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + currentFileType + "\\" + currentFileName, string.Empty);
			using (StreamWriter writer = File.AppendText(Directory.GetCurrentDirectory() + "\\" + currentFileType + "\\" + currentFileName))
			{
				foreach(var x in sudokuFileData)
				{
					writer.WriteLine(x);
				}

				//writer.WriteLine("\n");
				writer.WriteLine("LastEditedData");
				for (int i = 0; i < 9; i++)
				{
					string rows = "";
					for (int j = 0; j < 9; j++)
					{
						rows += sudokuEdited[i, j];
					}

					writer.WriteLine(rows);
				}
			}
						
			
		}		

		//Click method for the progress button
		private void progressBtn_Click(object sender, EventArgs e)
		{
			wrongTBs = new List<string>();
			isProgress = true;
			string text = "";
			int count = 0;
			string tbs = "";
			bool isFoundError = false;
			//Iterating thourgh all the textboxes
			for(int i = 0; i < 9; i++)
			{
				for( int j = 0; j < 9; j++)
				{
					//If the textbox is zero then incrementing the count of remaining textboxes to be filled
					if (sudokuEdited[i, j] == 0)
						count++;
					//If the user enterd value is incorrect ,storing the row and column of that tb in list
					if(sudokuEdited[i,j] != 0 && sudokuEdited[i,j] != sudokuAnswerinArray[i,j])
					{
						tbs = i + "-" + j;
						wrongTBs.Add(tbs);
						isFoundError = true;
					}
				}
			}		

			//Showing messages based on the result
			if (!isFoundError)
				text = "You're doing well so far!\nRemaining cells to be filled : " + count;
			else
			{
				DrawGrid();
				text = "We found some mistakes, Please re-check and change them.";
				isFoundError = false;
			}

			MessageBox.Show(text);
		}

		//Click method for pause button
		private void pauseBtn_Click(object sender, EventArgs e)
		{
			//If the button text is pause change it to resume and viceversa
			if (pauseBtn.Text == "Pause")
			{
				//Stopping the timer on pause and disabling the panel
				timer.Stop();
				pauseBtn.Text = "Resume";
				messagesRTB.Clear();
				_totalElapsedTime = new TimeSpan(timer.Elapsed.Hours, timer.Elapsed.Minutes, timer.Elapsed.Seconds);
				_totalElapsedTime = _totalElapsedTime + timeFromFile;
				messagesRTB.Text += "You Paused! Press resume button to resume the game.\n";
				messagesRTB.Text += "Time Elapsed :" + _totalElapsedTime;
				panel.Visible = false;				
			}
			else if(pauseBtn.Text == "Resume")
			{
				//On resume enabling the panel and changing the text
				timer.Start();
				pauseBtn.Text = "Pause";
				statusLB.Text = "";
				messagesRTB.Clear(); 
				panel.Visible = true;
			}
		}

		//Reset button funcationality
		private void resetBtn_Click(object sender, EventArgs e)
		{
			isReset = true;
			// Stop and reset the timer if it was running
			timer.Reset();			
			_timerRunning = true;
			sudokuEdited = new int[9, 9];			
			DrawGrid();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//Onload setting the default values
			level = "easy";
			selectLevelCB.SelectedItem = "Easy";			
			pauseBtn.Enabled = false;
			SaveBtn.Enabled = false;
			helpBtn.Enabled = false;
			resetBtn.Enabled = false;
			progressBtn.Enabled = false;
			statusLB.Font = new Font("Arial", 15);		
			statusLB.Location = new Point(0, 100);			
			statusLB.Text = "Select a difficulty and press New Game \nto begin!";
			//newGameBtn_Click(sender, new EventArgs());
		}

		//Click method for help button
		private void helpBtn_Click(object sender, EventArgs e)
		{
			isHelp = true;
			Random rnd = new Random();

			int i = rnd.Next(0, 9); // creates a number between 0 and 9
			int j = rnd.Next(0, 9);   // creates a number between 0 and 9
			while(sudokuEdited[i,j] != 0)
			{
				i = rnd.Next(0, 9); // creates a number between 0 and 9
				j = rnd.Next(0, 9);   // creates a number between 0 and 9				
			}

			//Taking a random textbox and assigning it a correct value 
			sudokuEdited[i, j] = sudokuAnswerinArray[i, j];
			DrawGrid();			
			MessageBox.Show("Hint : Value at " + (i + 1) + " row and " + (j + 1) + " col is " + sudokuAnswerinArray[i, j]);
		}		
		
	}
}
