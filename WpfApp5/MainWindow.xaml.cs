using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        // Список вопросов
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

        // Список ответов для каждого вопроса
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

        private int currentQuestionIndex = 0; // Индекс текущего вопроса
        private int correctAnswers = 0; // Количество правильных ответов
        private int totalQuestions; // Общее количество вопросов
        private int timeLimitSeconds = 60; // Ограничение времени в секундах

        public MainWindow()
        {
            InitializeComponent();
            totalQuestions = questions.Count; // Получаем общее количество вопросов
            PopulateQuestion(); // Заполняем первый вопрос
        }

        // Заполнение текстовых блоков вопросами и вариантами ответов
        private void PopulateQuestion()
        {
            if (currentQuestionIndex < totalQuestions) // Проверяем, что текущий вопрос не превышает общее количество вопросов
            {
                questionTextBlock.Text = questions[currentQuestionIndex]; // Устанавливаем текст текущего вопроса
                var options = answers[currentQuestionIndex]; // Получаем варианты ответов для текущего вопроса
                option1RadioButton.Content = options[0]; // Устанавливаем текст для первой радиокнопки
                option2RadioButton.Content = options[1]; // Устанавливаем текст для второй радиокнопки
                option3RadioButton.Content = options[2]; // Устанавливаем текст для третьей радиокнопки
                option4RadioButton.Content = options[3]; // Устанавливаем текст для четвертой радиокнопки
            }
        }

        // Обработчик события выбора радиокнопки
        private void OptionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            EvaluateAnswer(); // Оцениваем ответ пользователя
            currentQuestionIndex++; // Увеличиваем индекс текущего вопроса
            if (currentQuestionIndex < totalQuestions) // Проверяем, что есть еще вопросы
            {
                PopulateQuestion(); // Заполняем следующий вопрос
            }
            else
            {
                ShowTestResults(); // Показываем результаты теста
            }
        }

        // Оценка ответа пользователя
        private void EvaluateAnswer()
        {
            var selectedAnswer = GetSelectedAnswer(); // Получаем индекс выбранного ответа
            var correctAnswerIndex = Array.IndexOf(answers[currentQuestionIndex], answers[currentQuestionIndex][0]); // Получаем индекс правильного ответа
            if (selectedAnswer == correctAnswerIndex) // Проверяем, совпадает ли выбранный ответ с правильным
            {
                correctAnswers++; // Увеличиваем счетчик правильных ответов
            }
        }

        // Получение индекса выбранного ответа
        private int GetSelectedAnswer()
        {
            if (option1RadioButton.IsChecked == true)
                return 0; // Возвращаем индекс первого ответа
            else if (option2RadioButton.IsChecked == true)
                return 1; // Возвращаем индекс второго ответа
            else if (option3RadioButton.IsChecked == true)
                return 2; // Возвращаем индекс третьего ответа
            else if (option4RadioButton.IsChecked == true)
                return 3; // Возвращаем индекс четвертого ответа
            else
                return -1; // Никакой вариант не выбран
        }

        // Показ результатов теста
        private void ShowTestResults()
        {
            MessageBox.Show($"Тест завершен. Вы ответили правильно на {correctAnswers} из {totalQuestions} вопросов.",
                    "Результаты теста", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close(); // Закрываем окно после завершения теста
        }
    }
}
