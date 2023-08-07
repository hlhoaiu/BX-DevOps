﻿using DevOps.Helpers;
using DevOps.Logger;
using DevOps.Models.Config;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DevOps.Services.Form
{
    public class ReplaceWordContentService : IReplaceWordContentService
    {
        private readonly ILogger _logger;

        public ReplaceWordContentService(ILogger logger)
        {
            _logger = logger;
        }

        public void Replace(IDictionary<string, string> replaceDict, string saveAsPath) 
        {
            if (replaceDict == null || replaceDict.Count == 0) 
            {
                _logger.Log("Nothing need to be replaced.");
                return;
            }

            var fileFullPath = PathHelper.TemplateImplFormPath;
            _logger.Log($"Started to replace string in Word Document. Total string to replace: {replaceDict.Count}. File Path: {fileFullPath}");
            Application wordApp = new Application { Visible = false};
            try
            {
                Document wordDoc = wordApp.Documents.Open(fileFullPath, ReadOnly: false, Visible: true);
                foreach (var item in replaceDict)
                {
                    FindAndReplace(wordApp, item.Key, item.Value);
                }
                wordDoc.SaveAs(saveAsPath);
                wordDoc.Close();
                _logger.Log($"All strings were replaced in Word Document. File saved to: {saveAsPath}");
            }
            catch (global::System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            finally 
            {
                wordApp.Quit();
                GC.Collect();
            }
        }

        private void FindAndReplace(Application doc, object findText, object replaceWithText)
        {
            //options
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace
            doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }
    }
}
