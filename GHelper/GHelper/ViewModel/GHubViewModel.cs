﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GHelper.Utility;
using GHelperLogic.Model;
using GHelperLogic.Utility;

namespace GHelper.ViewModel
{
	public class GHubViewModel
	{
		public GHubSettingsFile                           GHubSettingsFile { get; }
		public ObservableCollection<ApplicationViewModel> Applications     { get; } = new ();

		public GHubViewModel(GHubSettingsFile gHubSettingsFile)
		{
			GHubSettingsFile = gHubSettingsFile;
			InitializeApplications();
		}

		public void Delete(GHubRecordViewModel recordViewModel)
        {
            switch (recordViewModel.GHubRecord)
            {
                case Application application:
                    DeleteApplication(application);
                    break;
                case Profile profile:
                    DeleteProfile(profile);
                    break;
            }

            InitializeApplications();
        }

		public void SetInitialRecordStates()
		{
			foreach (ApplicationViewModel application in Applications)
			{
				application.SaveBackup();

				foreach (ProfileViewModel profile in application.Profiles)
				{
					profile.SaveBackup();
				}
			}
		}

		private void DeleteApplication(Application application)
		{
			GHubSettingsFile.Applications?.Applications?.Remove(application);
		}

		private void DeleteProfile(Profile profile)
		{
			GHubSettingsFile.Profiles?.Profiles?.Remove(profile);
		}

		private void InitializeApplications()
		{
			GHubSettingsFile.AssociateProfilesToApplications();
			ICollection<Application>? applications = GHubSettingsFile.Applications?.Applications;
			Applications.ReplaceAll(ApplicationViewModel.CreateFromCollection(applications));
			Applications.Sort(SortApplications);
		}

		private static int SortApplications(ApplicationViewModel first, ApplicationViewModel second)
		{
			if (first.Application is DesktopApplication)
			{
				return -1;
			}
			else if (second.Application is DesktopApplication)
			{
				return 1;
			}
			else if (first.Application?.Name is string firstApplicationName && second.Application?.Name is string secondApplicationName)
			{
				return string.Compare(firstApplicationName, secondApplicationName, StringComparison.Ordinal);
			}
			else
			{
				return 0;
			}
		}
	}
}