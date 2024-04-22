using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        private List<string> questions = new List<string>()
        {
            "Какой тип данных используется для хранения целых чисел?",
            "Какой тип данных используется для хранения дробных чисел с плавающей точкой?",
            "Какой тип данных используется для хранения логических значений?",
            "Какой тип данных используется для хранения символов?",
            "Какой тип данных используется для хранения текстовых строк?",
            "Какой тип данных используется для хранения даты и времени?",
            "Какой тип данных используется для хранения больших объемов числовых данных?",
            "Какой тип данных используется для хранения небольших целых чисел?",
            "Какой тип данных используется для хранения адресов памяти?",
            "Какой тип данных используется для хранения одного из набора предопределенных значений?"
        };

        private List<string[]> answers = new List<string[]>()
        {
            new string[] { "int", "float", "bool", "char" },
            new string[] { "float", "double", "decimal", "int" },
            new string[] { "bool", "int", "string", "char" },
            new string[] { "char", "int", "string", "bool" },
            new string[] { "string", "int", "bool", "char" },
            new string[] { "DateTime", "string", "int", "double" },
            new string[] { "decimal", "double", "int", "float" },
            new string[] { "byte", "int", "decimal", "double" },
            new string[] { "IntPtr", "string", "int", "bool" },
            new string[] { "enum", "int", "string", "float" }
        };

        private int currentQuestionIndex = 0;
        private int correctAnswers = 0;
        private int totalQuestions;
        private int timeLimitSeconds = 60;
        private bool isTestRunning = false;
        private Task timerTask;

        public MainWindow()
        {
            InitializeComponent();
            totalQuestions = questions.Count;
        }

        private void StartTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isTestRunning)
            {
                isTestRunning = true;
                startTestButton.Visibility = Visibility.Collapsed; // Скрыть кнопку начала теста
                timerTextBlock.Visibility = Visibility.Visible;
                questionTextBlock.Visibility = Visibility.Visible;
                option1RadioButton.Visibility = Visibility.Visible;
                option2RadioButton.Visibility = Visibility.Visible;
                option3RadioButton.Visibility = Visibility.Visible;
                option4RadioButton.Visibility = Visibility.Visible;
                nextButton.Visibility = Visibility.Visible;
                StartTimer();
                PopulateQuestion();
            }
        }

        private async void StartTimer()
        {
            int remainingSeconds = timeLimitSeconds;
            while (remainingSeconds >= 0 && isTestRunning)
            {
                timerTextBlock.Text = $"Оставшееся время: {remainingSeconds} секунд";
                await Task.Delay(1000);
                remainingSeconds--;
            }
            if (isTestRunning)
            {
                isTestRunning = false;
                ShowTestResults();
            }
        }

        private void PopulateQuestion()
        {
            if (currentQuestionIndex < totalQuestions)
            {
                questionTextBlock.Text = $"{currentQuestionIndex + 1}. {questions[currentQuestionIndex]}";
                var options = answers[currentQuestionIndex];
                option1RadioButton.Content = options[0];
                option2RadioButton.Content = options[1];
                option3RadioButton.Content = options[2];
                option4RadioButton.Content = options[3];
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            EvaluateAnswer();
            currentQuestionIndex++;
            if (currentQuestionIndex < totalQuestions)
            {
                PopulateQuestion();
            }
            else
            {
                ShowTestResults();
            }
        }

        private void EvaluateAnswer()
        {
            var selectedAnswer = GetSelectedAnswer();
            var correctAnswerIndex = Array.IndexOf(answers[currentQuestionIndex], answers[currentQuestionIndex][0]);
            if (selectedAnswer == correctAnswerIndex)
            {
                correctAnswers++;
            }
        }

        private int GetSelectedAnswer()
        {
            if (option1RadioButton.IsChecked == true)
                return 0;
            else if (option2RadioButton.IsChecked == true)
                return 1;
            else if (option3RadioButton.IsChecked == true)
                return 2;
            else if (option4RadioButton.IsChecked == true)
                return 3;
            else
                return -1;
        }

        private void ShowTestResults()
        {
            MessageBox.Show($"Тест завершен. Вы ответили правильно на {correctAnswers} из {totalQuestions} вопросов.",
                    "Результаты теста", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
