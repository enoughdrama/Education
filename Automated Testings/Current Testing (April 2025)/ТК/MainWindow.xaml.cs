using System;
using System.Windows;

namespace GradeCalculatorForEachModule
{
    public class ExactCalculator
    {
        public const int MaxDBModuleScore = 22;
        public const int MaxSFModuleScore = 38;
        public const int MaxMSModuleScore = 20;
        public const int MaxTotal = MaxDBModuleScore + MaxSFModuleScore + MaxMSModuleScore;

        public int ValidateGathering(string input, int maxScore)
        {
            if (!int.TryParse(input, out int score))
            {
                throw new ArgumentException("Значение балла должно быть целочисленным.");
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

        public string Compute(int totalScore)
        {
            if (totalScore >= 56 && totalScore <= 80)
                return "5 -> Отлично";
            else if (totalScore >= 32 && totalScore <= 55)
                return "4 -> Хорошо";
            else if (totalScore >= 16 && totalScore <= 31)
                return "3 -> Удовлетворительно";
            else
                return "2 -> Неудовлетворительно";
        }
    }

    public partial class MainWindow : Window
    {
        private readonly ExactCalculator calc;

        public MainWindow()
        {
            InitializeComponent();
            calc = new ExactCalculator();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int DBScore = calc.ValidateGathering(DBInput.Text, ExactCalculator.MaxDBModuleScore);
                int SFScore = calc.ValidateGathering(SFInput.Text, ExactCalculator.MaxSFModuleScore);
                int MSScore = calc.ValidateGathering(MSInput.Text, ExactCalculator.MaxMSModuleScore);

                int totalScore = DBScore + SFScore + MSScore;
                string grade = calc.Compute(totalScore);

                TotalTextbox.Text = totalScore.ToString();
                GradeTextbox.Text = grade;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}