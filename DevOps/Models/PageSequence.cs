﻿using DevOps.Views;
using System;
using System.Collections.Generic;

namespace DevOps.Models
{
    public static class PageSequence
    {
        public static IList<string> Sequence = new List<string> 
        {
            nameof(ConfigPage),
            nameof(PackagePage),
            nameof(FormPage),
            nameof(BackupPage),
            nameof(DeployPage)
        };
        public static IDictionary<string, string> NameMapping = new Dictionary<string, string>
        {
            { nameof(ConfigPage), "Config Page" },
            { nameof(PackagePage), "Package Page" },
            { nameof(FormPage), "Form Page" },
            { nameof(BackupPage), "Backup Page" },
            { nameof(DeployPage), "Deploy Page" }
        };

        private static string PageSeparater => "\n-------------\n";

        public static string GetNextPageName(string currentPageName) 
        {
            var currentPageIndex = Sequence.IndexOf(currentPageName);
            if (currentPageIndex == -1) 
            {
                throw new ArgumentException($"Input a wrong page name for PageSequence. Wrong Page Name:{currentPageName}");
            }
            return currentPageIndex + 1 < Sequence.Count ? NameMapping[Sequence[currentPageIndex + 1]] : "Exit";
        }

        public static string GetOnNextPageLog(string currentPageName) 
        {
            return $"{PageSeparater}From [{NameMapping[currentPageName]}] go to [{GetNextPageName(currentPageName)}]";
        }

        public static string GetBackPageName(string currentPageName)
        {
            var currentPageIndex = Sequence.IndexOf(currentPageName);
            if (currentPageIndex == -1)
            {
                throw new ArgumentException($"Input a wrong page name for PageSequence. Wrong Page Name:{currentPageName}");
            }
            return currentPageIndex - 1 >= 0 ? NameMapping[Sequence[currentPageIndex - 1]] : "Init";
        }

        public static string GetOnBackPageLog(string currentPageName)
        {
            return $"{PageSeparater}From [{NameMapping[currentPageName]}] go back to [{GetBackPageName(currentPageName)}]";
        }
    }
}
