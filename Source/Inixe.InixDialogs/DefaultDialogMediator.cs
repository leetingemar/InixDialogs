﻿/*
							MIT License

Copyright (c) 2016 Ingemar Parra H.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

namespace Inixe.InixDialogs
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Input;


	public class DefaultDialogMediator : IDialogMediator, IRelayMediator
	{
		private IDialogMediator _relayed;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultMediator"/> class.
		/// </summary>
		public DefaultDialogMediator()
		{
			_relayed = new NullDialogMediator();
		}
		
		internal event EventHandler<ShowEventArgs> Show;

		IDialogMediator IRelayMediator.Relayer
		{
			get 
			{ 
				return _relayed; 
			}
		}

		void IRelayMediator.AddRelayer(IDialogMediator mediator)
		{
			mediator.ThrowIfNull("mediator");

			if (_relayed!=null)
			{
				IRelayMediator relayedMediator = _relayed as IRelayMediator;

				if (relayedMediator != null)
				{
					relayedMediator.AddRelayer(mediator);
					return;
				}
			}
			
			_relayed = mediator;
		}

		/// <summary>
		/// Shows a dialog on the current WPF page
		/// </summary>
		/// <param name="nextActionCommand">If the dialog is not canceled the nextActionCommand will be executed</param>
		/// <param name="otherActionCommand">If the dialog is canceled then this action is executed.</param>
		/// <param name="state">Optional. When an actionCommand is executed the state parameter is handed over to the actionCommand</param>
		/// <param name="settings">The dialog configuration settings <seealso cref="DialogSettingsBase" /></param>
		public void ShowDialog(ICommand nextActionCommand, ICommand otherActionCommand, object state, DialogSettingsBase settings)
		{
			_relayed.ShowDialog(nextActionCommand, otherActionCommand, state, settings);
		}

		/// <summary>
		/// Shows a yes/no dialog on the current WPF page
		/// </summary>
		/// <param name="yesActionCommand">If the yes option is selected this command is Executed.</param>
		/// <param name="noActionCommand">If the yes option is selected this command is Executed.</param>
		/// <param name="otherActionCommand">If the dialog is canceled then this action is executed.</param>
		/// <param name="state">Optional. When an actionCommand is executed the state parameter is handed over to the actionCommand</param>
		/// <param name="settings">The dialog configuration settings <seealso cref="DialogSettingsBase" /></param>
		public void ShowDialog(ICommand yesActionCommand, ICommand noActionCommand, ICommand otherActionCommand, object state, DialogSettingsBase settings)
		{
			_relayed.ShowDialog(yesActionCommand, noActionCommand, otherActionCommand, state, settings);
		}
	}
}