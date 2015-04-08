﻿/*Copyright (c) 2015, Nordic Semiconductor ASA
 *
 *Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
 *
 *1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 *
 *2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other 
 *materials provided with the distribution.
 *
 *3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific 
 *prior written permission.
 *
 *THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 *PURPOSE ARE DISCLAIMED. *IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF *SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, *DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 *ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED *OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace nRFToolbox.Common
{
	public class SmartDispatcherTimer : DispatcherTimer
	{
		public bool IsReentrant { get; set; }
		public bool IsRunning { get; private set; }

		public Action TickTask { get; set; }

		public SmartDispatcherTimer()
		{
			base.Tick += SmartDispatcherTimer_Tick;
		}

		private void SmartDispatcherTimer_Tick(object sender, object e)
		{
			if (TickTask == null)
			{
				//Debug.WriteLine("No task set!");
				return;
			}

			if (IsRunning && !IsReentrant)
			{
				// previous task hasn't completed
				//Debug.WriteLine("Task already running");
				return;
			}

			try
			{
				// we're running it now
				IsRunning = true;
				//Debug.WriteLine("Running Task");
				 TickTask.Invoke();
				//Debug.WriteLine("Task Completed");
			}
			catch (Exception)
			{
				//Debug.WriteLine("Task Failed");
			}
			finally
			{
				// allow it to run again
				IsRunning = false;
			}
		}

	}
}
