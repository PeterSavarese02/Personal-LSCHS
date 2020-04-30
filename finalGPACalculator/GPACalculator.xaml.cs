using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace finalGPACalculator
{
    /// <summary>
    /// Interaction logic for GPACalculator.xaml
    /// </summary>
    public partial class GPACalculator : Window
    {
        public GPACalculator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called in try;catch and if checks. Displays popup box
        /// </summary>
        /// <param name="mMessage"></param>
        public void ErrorCatcher(string mMessage)
        {
            if (mMessage == "")
            {
                return;
            }

            MessageBox.Show("Error:\n" + mMessage, "Error Catcher", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Simple math.Clamp function. Doesn't allow int to fall below <paramref name="min"/> and above <paramref name="max"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        /// <summary>
        /// Calculates the GPA for the specified class.
        /// </summary>
        /// <param name="dProvidedPerc">Percentage grade for the class we are claulcaating</param>
        /// <param name="lblGPA">The Label of the GPA to the right of the row. This is changed at the end when we calculate the GPA for this class</param>
        /// <param name="classType">Whether this class is a regular, honors, or A.P.</param>
        /// <returns></returns>
        private double convertPercToGPA(int dProvidedPerc, Label lblGPA, ComboBox classType)
        {
            double dReturnValue = 0.00; // The GPA of the provided dProvidedPerc
            dProvidedPerc = Clamp(dProvidedPerc, 0, 100);

            // Begin clustertruck of conversion
            if (dProvidedPerc >= 97 & dProvidedPerc <= 100)
                dReturnValue = 4.3;

            if (dProvidedPerc >= 93 & dProvidedPerc <= 97)
                dReturnValue = 4.0;

            if (dProvidedPerc >= 90 & dProvidedPerc < 93)
                dReturnValue = 3.7;

            if (dProvidedPerc < 90 & dProvidedPerc >= 88)
                dReturnValue = 3.3;

            if (dProvidedPerc < 88 & dProvidedPerc >= 85)
                dReturnValue = 3.0;

            if (dProvidedPerc < 85 & dProvidedPerc >= 82)
                dReturnValue = 2.7;

            if (dProvidedPerc < 82 & dProvidedPerc >= 80)
                dReturnValue = 2.3;

            if (dProvidedPerc < 80 & dProvidedPerc >= 77)
                dReturnValue = 2.0;

            if (dProvidedPerc < 77 & dProvidedPerc >= 74)
                dReturnValue = 1.7;

            if (dProvidedPerc < 74 & dProvidedPerc >= 72)
                dReturnValue = 1.3;

            if (dProvidedPerc < 72 & dProvidedPerc >= 70)
                dReturnValue = 1.0;

            if (dProvidedPerc < 70)
                dReturnValue = 0.0;

            if (classType.SelectedIndex == 1)
            {
                // Honors boost
                dReturnValue += 0.5;
            } else if (classType.SelectedIndex == 2)
            {
                // A.P. Boost
                dReturnValue += 1;
            }

            if (dProvidedPerc == 0)
            {
                // Don't do anything, we never entered a grade.
                dReturnValue = 0;
            }

           lblGPA.Content = Convert.ToString(dReturnValue); // Set what the GPA for the individual class is

            return dReturnValue;
        }

        /// <summary>
        /// Go through all rows and calculate the total GPA
        /// TODO: Put this into a for loop instead of making messy/repeating code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getGPA_click(object sender, EventArgs e)
        {
            // Begin grade conversion
            double dGPA1 = convertPercToGPA(Convert.ToInt32(txtScore1.Text), lblGPA, cb1);
            double dGPA2 = convertPercToGPA(Convert.ToInt32(txtScore2.Text), lblGPA1, cb2);
            double dGPA3 = convertPercToGPA(Convert.ToInt32(txtScore3.Text), lblGPA2, cb3);
            double dGPA4 = convertPercToGPA(Convert.ToInt32(txtScore4.Text), lblGPA3, cb4);
            double dGPA5 = convertPercToGPA(Convert.ToInt32(txtScore5.Text), lblGPA4, cb5);
            double dGPA6 = convertPercToGPA(Convert.ToInt32(txtScore6.Text), lblGPA5, cb6);
            double dGPA7 = convertPercToGPA(Convert.ToInt32(txtScore7.Text), lblGPA6, cb7);
            double dGPA8 = convertPercToGPA(Convert.ToInt32(txtScore8.Text), lblGPA7, cb8);
            double dGPA9 = convertPercToGPA(Convert.ToInt32(txtScore9.Text), lblGPA8, cb9);

            // Inline conditional to check if that class has a grade or not
            double dCredits1 = txtScore1.Text != "0" ? Convert.ToDouble(cb10.Text) : 0.00;
            double dCredits2 = txtScore2.Text != "0" ? Convert.ToDouble(cb11.Text) : 0.00;
            double dCredits3 = txtScore3.Text != "0" ? Convert.ToDouble(cb12.Text) : 0.00;
            double dCredits4 = txtScore4.Text != "0" ? Convert.ToDouble(cb13.Text) : 0.00;
            double dCredits5 = txtScore5.Text != "0" ? Convert.ToDouble(cb14.Text) : 0.00;
            double dCredits6 = txtScore6.Text != "0" ? Convert.ToDouble(cb15.Text) : 0.00;
            double dCredits7 = txtScore7.Text != "0" ? Convert.ToDouble(cb16.Text) : 0.00;
            double dCredits8 = txtScore8.Text != "0" ? Convert.ToDouble(cb17.Text) : 0.00;
            double dCredits9 = txtScore9.Text != "0" ? Convert.ToDouble(cb18.Text) : 0.00;

            double dFinalGPA = (dGPA1 + dGPA2 + dGPA3 + dGPA4 + dGPA5 + dGPA6 + dGPA7 + dGPA8 + dGPA9) / (dCredits1 + dCredits2 + dCredits3 + dCredits4 + dCredits5 + dCredits6 + dCredits7 + dCredits8 + dCredits9);
            FinalGPA.Content = !Double.IsNaN(dFinalGPA) ? dFinalGPA : 0.00;

            // FinalScore = ((Score1) + (Score2) + (Score3) + (Score4) + (Score5) + (Score6) + (Score7) + (Score8) + (Score9)) / ((Credit1) + (Credit2) + (Credit3) + (Credit4) + (Credit5) + (Credit6) + (Credit7) + (Credit8) + (Credit9));

            // FinalGPA.Content = System.Convert.ToString(Math.Round(FinalScore, 2));
        }

        /// <summary>
        /// Get each class' percentage grade and the class type (A.P., honors) and store it in a JSon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveGradesButton_click(object sender, RoutedEventArgs e)
        {
            double[,] iGrades = new double[9,2];
            iGrades[0, 0] = Convert.ToDouble(txtScore1.Text);
            iGrades[0, 1] = Convert.ToDouble(cb1.SelectedIndex);

            iGrades[1, 0] = Convert.ToDouble(txtScore2.Text);
            iGrades[1, 1] = Convert.ToDouble(cb2.SelectedIndex);

            iGrades[2, 0] = Convert.ToDouble(txtScore3.Text);
            iGrades[2, 1] = Convert.ToDouble(cb3.SelectedIndex);

            iGrades[3, 0] = Convert.ToDouble(txtScore4.Text);
            iGrades[3, 1] = Convert.ToDouble(cb4.SelectedIndex);

            iGrades[4, 0] = Convert.ToDouble(txtScore5.Text);
            iGrades[4, 1] = Convert.ToDouble(cb5.SelectedIndex);

            iGrades[5, 0] = Convert.ToDouble(txtScore6.Text);
            iGrades[5, 1] = Convert.ToDouble(cb6.SelectedIndex);

            iGrades[6, 0] = Convert.ToDouble(txtScore7.Text);
            iGrades[6, 1] = Convert.ToDouble(cb7.SelectedIndex);

            iGrades[7, 0] = Convert.ToDouble(txtScore8.Text);
            iGrades[7, 1] = Convert.ToDouble(cb8.SelectedIndex);

            iGrades[8, 0] = Convert.ToDouble(txtScore9.Text);
            iGrades[8, 1] = Convert.ToDouble(cb9.SelectedIndex);

            String sGradesInJson = JsonConvert.SerializeObject(iGrades);

            Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "petersav"));
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/petersav/grades.json", sGradesInJson);
        }

        /// <summary>
        /// Load the JSon file for persons grades and populate the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadGradesButton_click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/petersav/grades.json"))
            {
                ErrorCatcher("Attempt to load grades.json (File doesn't exist)");

                return;
            }

            String sFileTxt = "";

            var fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/petersav/grades.json", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                sFileTxt = streamReader.ReadToEnd();
            }

            int iCurLoopPos = 0; // Hacky way to track our index count
            JArray JSON = JArray.Parse(sFileTxt);
            double[,] iGrades = new double[9, 2];

            // Hacky way to get multidimensional arrays to work. Loop through the JArray and slect index 0 and 1, add it to the multidimens array above, and use iGrades in the future
            foreach (var item in JSON.Children())
            {
                var itemProperties = item.Children<JProperty>();
                iGrades[iCurLoopPos, 0] = Convert.ToDouble(item[0]);
                iGrades[iCurLoopPos, 1] = Convert.ToDouble(item[1]);
                iCurLoopPos++;
            }

            txtScore1.Text = Convert.ToString(iGrades[0, 0]);
            cb1.SelectedIndex = Convert.ToInt32(iGrades[0, 1]);

            txtScore2.Text = Convert.ToString(iGrades[1, 0]);
            cb2.SelectedIndex = Convert.ToInt32(iGrades[1, 1]);

            txtScore3.Text = Convert.ToString(iGrades[2, 0]);
            cb3.SelectedIndex = Convert.ToInt32(iGrades[2, 1]);

            txtScore4.Text = Convert.ToString(iGrades[3, 0]);
            cb4.SelectedIndex = Convert.ToInt32(iGrades[3, 1]);

            txtScore5.Text = Convert.ToString(iGrades[4, 0]);
            cb5.SelectedIndex = Convert.ToInt32(iGrades[4, 1]);

            txtScore6.Text = Convert.ToString(iGrades[5, 0]);
            cb6.SelectedIndex = Convert.ToInt32(iGrades[5, 1]);

            txtScore7.Text = Convert.ToString(iGrades[6, 0]);
            cb7.SelectedIndex = Convert.ToInt32(iGrades[6, 1]);

            txtScore8.Text = Convert.ToString(iGrades[7, 0]);
            cb8.SelectedIndex = Convert.ToInt32(iGrades[7, 1]);

            txtScore9.Text = Convert.ToString(iGrades[7, 0]);
            cb9.SelectedIndex = Convert.ToInt32(iGrades[8, 1]);

            getGPA_click(null, null); // Go calculate the GPA now

            // Begin setting txt inputs to the loaded JSon
            //txtScore1.Text = Convert.ToString(dGrades);
        }
    }
}
