using System;
using System.Windows;

namespace StudentGradeCalculator
{
    public class GradeCalculator
    {
        public const int MaxDatabaseScore = 22;
        public const int MaxSoftwareScore = 38;
        public const int MaxMaintenanceScore = 20;
        public const int MaxTotalScore = MaxDatabaseScore + MaxSoftwareScore + MaxMaintenanceScore;

        public int ValidateAndGetScore(string input, int maxScore)
        {
            if (!int.TryParse(input, out int score))
            {
                throw new ArgumentException("Пожалуйста, введите целое число для баллов.");
            }

            if (score < 0)
            {
                throw new ArgumentException("Баллы не могут быть отрицательными.");
            }

            if (score > maxScore)
            {
                throw new ArgumentException($"Максимальное количество баллов: {maxScore}.");
            }

            return score;
        }

        public string GetGrade(int totalScore)
        {
            if (totalScore >= 56 && totalScore <= 80)
                return "«5» (отлично)";
            else if (totalScore >= 32 && totalScore <= 55)
                return "«4» (хорошо)";
            else if (totalScore >= 16 && totalScore <= 31)
                return "«3» (удовлетворительно)";
            else
                return "«2» (неудовлетворительно)";
        }
    }

    public partial class MainWindow : Window
    {
        private readonly GradeCalculator calculator;

        public MainWindow()
        {
            InitializeComponent();
            calculator = new GradeCalculator();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int databaseScore = calculator.ValidateAndGetScore(txtDatabaseScore.Text, GradeCalculator.MaxDatabaseScore);
                int softwareScore = calculator.ValidateAndGetScore(txtSoftwareScore.Text, GradeCalculator.MaxSoftwareScore);
                int maintenanceScore = calculator.ValidateAndGetScore(txtMaintenanceScore.Text, GradeCalculator.MaxMaintenanceScore);

                int totalScore = databaseScore + softwareScore + maintenanceScore;
                string grade = calculator.GetGrade(totalScore);

                txtTotalScore.Text = totalScore.ToString();
                txtGrade.Text = grade;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
