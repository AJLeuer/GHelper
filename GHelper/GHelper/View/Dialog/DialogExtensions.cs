﻿using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Nzr.ToolBox.Core;

namespace GHelper.View.Dialog
{
	public static class DialogExtensions
	{
		public static async Task<ContentDialogResult> EnqueueAndShowIfAsync(this ContentDialog contentDialog)
		{
			TaskCompletionSource<ObjectUtils.Null> currentDialogCompletion = new ();
			TaskCompletionSource<ObjectUtils.Null>? previousDialogCompletion = PreviousDialogCompletion;
			PreviousDialogCompletion = currentDialogCompletion;

			if (previousDialogCompletion != null) 
			{
				await previousDialogCompletion.Task;
			}

			ContentDialogResult buttonPressed = await contentDialog.ShowAsync();

			currentDialogCompletion.SetResult(null!);
			return buttonPressed;
		}

		private static TaskCompletionSource<ObjectUtils.Null>? PreviousDialogCompletion = null;
	}
}