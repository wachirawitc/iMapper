﻿using EnvDTE;
using EnvDTE80;
using iMapper.Constance;
using iMapper.Extensions;
using iMapper.Model;
using iMapper.Repository;
using iMapper.Support;
using iMapper.Template.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iMapper.Forms
{
    [System.Runtime.InteropServices.Guid("F3C11D52-4077-4075-BCE0-D8EE88F5A5BE")]
    public partial class TransferForm : Form
    {
        private readonly ProjectItem projectItem;
        private readonly DTE2 dte2;

        private readonly TemporaryRepository temporaryRepository;

        public TransferForm(ProjectItem projectItem, DTE2 dte2)
        {
            InitializeComponent();
            this.projectItem = projectItem;
            this.dte2 = dte2;

            temporaryRepository = new TemporaryRepository();
        }

        private void TransferForm_Load(object sender, EventArgs e)
        {
            InitAutoComplete();
        }

        private void InitAutoComplete()
        {
            var models = temporaryRepository.GetTransfer();
            if (models.Any())
            {
                var fullNames = models.Select(x => x.FullName).ToArray();
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(fullNames);

                Source.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                Source.AutoCompleteSource = AutoCompleteSource.CustomSource;
                Source.AutoCompleteCustomSource = collection;

                Destination.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                Destination.AutoCompleteSource = AutoCompleteSource.CustomSource;
                Destination.AutoCompleteCustomSource = collection;
            }
        }

        private void OnClickReloadClass(object sender, EventArgs e)
        {
            new LoadForm(() =>
            {
                if (dte2 != null)
                {
                    var classElements = dte2.GetClass();
                    if (classElements.Any())
                    {
                        temporaryRepository.SetTransfer(classElements);

                        Invoke((MethodInvoker)InitAutoComplete);
                    }

                    Invoke((MethodInvoker)delegate
                    {
                        string message = $"Found {classElements.Count} class.";
                        MessageBox.Show(message, Text);
                    });
                }
            }).ShowDialog();
        }

        private void OnClickSave(object sender, EventArgs e)
        {
            var models = temporaryRepository.GetTransfer();

            var source = models.FirstOrDefault(x => x.FullName.Equals(Source.Text));
            var destination = models.FirstOrDefault(x => x.FullName.Equals(Destination.Text));

            if (source == null)
            {
                MessageBox.Show("Not found source class.");
            }

            if (destination == null)
            {
                MessageBox.Show("Not found destination class.");
            }

            if (source != null && destination != null)
            {
                string name = $"Map{source.Name}To{destination.Name}";

                var template = new DefaultTransferTemplate();
                template.Namespace = NamespaceHelper.Get(projectItem.ContainingProject, projectItem);
                template.Name = name;
                template.Source = source;
                template.Destination = destination;
                var code = template.TransformText();

                var fileName = $"{name}.cs";
                var destinationPath = projectItem.Properties.Item("FullPath").Value as string;
                var originalFile = new FileInfo($@"{destinationPath}{fileName}");

                var sourceCode = new SourceManage(fileName, code);
                var sourceFile = sourceCode.Create();

                var projectItemFile = projectItem.ProjectItems
                            .GetFiles()
                            .FirstOrDefault(x => x.Name == sourceFile.Name);

                if (projectItemFile != null)
                {
                    var outputFile = new FileInfo(Temporary.Directory + $"_{fileName}");
                    outputFile.DeleteIfExisting();
                    outputFile.CreateAndDispose();

                    var beforeModificationDate = outputFile.GetLastWriteTime();

                    string command = $"\"{sourceFile.FullName}\" \"{originalFile.FullName}\" -o \"{outputFile.FullName}\"";
                    var process = System.Diagnostics.Process.Start(temporaryRepository.Kdiff.FullName, command);
                    if (process != null)
                    {
                        process.WaitForExit();

                        var afterModificationDate = outputFile.GetLastWriteTime();
                        if (beforeModificationDate.IsEarlierThan(afterModificationDate))
                        {
                            sourceFile.DeleteIfExisting();
                            File.Copy(outputFile.FullName, sourceFile.FullName);

                            projectItemFile.Delete();
                            projectItem.ProjectItems.AddFromFileCopy(sourceFile.FullName);
                            projectItem.ContainingProject.Save();
                        }
                    }
                }
                else
                {
                    originalFile.DeleteIfExisting();
                    projectItem.ProjectItems.AddFromFileCopy(sourceFile.FullName);
                    projectItem.ContainingProject.Save();
                }

                Close();
            }
        }

        private void OnClickClose(object sender, EventArgs e)
        {
            Close();
        }
    }
}