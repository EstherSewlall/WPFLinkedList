using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LinkedList
{
    public partial class MainWindow : Window
    {
        private LinkedListNode<Pages> currentPage;
        private LinkedList<Pages> pages;
        private int pageNumber = 1;

        public MainWindow()
        {
            InitializeComponent();
            InitializePages();
            ShowPageContent(currentPage);
        }

        private void InitializePages()
        {
            pages = new LinkedList<Pages>();
            Pages firstPg = new Pages() { Content = "Nowadays no one uses the library..." };
            Pages secondPg = new Pages() { Content = "Instead of Google and Chat GPT is the space for research..." };
            Pages thirdPg = new Pages() { Content = "Libraries use a system of storing books" };
            Pages forthPg = new Pages() { Content = "...Known as the Dewey Decimal System" };
            Pages fifthPg = new Pages() { Content = "...of numbering...each book and category is given specific numbers" };
            Pages sixthPg = new Pages() { Content = "...and subjects areas..." };

            pages.AddLast(secondPg);
            LinkedListNode<Pages> nodepgFour = pages.AddLast(forthPg);
            pages.AddLast(sixthPg);
            pages.AddLast(firstPg);
            pages.AddBefore(nodepgFour, thirdPg);
            pages.AddAfter(nodepgFour, fifthPg);

            currentPage = pages.First;
        }

        private void ShowPageContent(LinkedListNode<Pages> pageNode)
        {
            PageContentTextBlock.Text = string.Empty;

            string numString = $"- {pageNumber} -";
            int leadSpaces = (90 - numString.Length) / 2;
            PageNumberTextBlock.Text = numString.PadLeft(leadSpaces + numString.Length);

            string content = pageNode.Value.Content;
            for (int i = 0; i < content.Length; i += 90)
            {
                string line = content.Substring(i);
                line = line.Length > 90 ? line.Substring(0, 90) : line;
                PageContentTextBlock.Text += line + Environment.NewLine;
            }

            AuthorTextBlock.Text = $"Author \"E Sewlall\", {Environment.NewLine} published 8/8/23";
            PreviousButton.IsEnabled = pageNode.Previous != null;
            NextButton.IsEnabled = pageNode.Next != null;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage.Previous != null)
            {
                currentPage = currentPage.Previous;
                pageNumber--;
                ShowPageContent(currentPage);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage.Next != null)
            {
                currentPage = currentPage.Next;
                pageNumber++;
                ShowPageContent(currentPage);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.N:
                    if (currentPage.Next != null)
                    {
                        currentPage = currentPage.Next;
                        pageNumber++;
                        ShowPageContent(currentPage);
                    }
                    break;

                case Key.P:
                    if (currentPage.Previous != null)
                    {
                        currentPage = currentPage.Previous;
                        pageNumber--;
                        ShowPageContent(currentPage);
                    }
                    break;

                default:
                    return;
            }
        }
    }

    public class Pages
    {
        public string Content { get; set; }
    }
}
