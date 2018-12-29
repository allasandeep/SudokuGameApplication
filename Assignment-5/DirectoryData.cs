/*******************************************************************************************************************
 *                                                                                                                 *
 *  CSCI 473/504							Assignment 5 								 Fall 2018                 *                                           
 *																										           *
 *  Programmer's: Sandeep Alla (z1821331)  *  
 *																										           *
 *  Date Due  : November 15, 2018			File :	Form1.cs     					     				           *                          
 *																										           *
 *  Purpose   : A Form application for Suduko Puzzle game													       *
 *																							                       *
 ******************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5
{
	class DirectoryData
	{
		//Variables
		string type;
		string fileName;
		string status;
		string timeElapsed;

		//Constructor
		public DirectoryData(string type, string fileName, string status, string timeElapsed)
		{
			this.type = type;
			this.fileName = fileName;
			this.status = status;
			this.timeElapsed = timeElapsed;
		}

		public string Type // Name method
		{
			get { return type; } //Get property
			set { type = value; } // Set property
		}

		public string FileName // Name method
		{
			get { return fileName; } //Get property
			set { fileName = value; } // Set property
		}

		public string Status // Name method
		{
			get { return status; } //Get property
			set { status = value; } // Set property
		}

		public string TimeElapsed // Name method
		{
			get { return timeElapsed; } //Get property
			set { timeElapsed = value; } // Set property
		}
	}
}
