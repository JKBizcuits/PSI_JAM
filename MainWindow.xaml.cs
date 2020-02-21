using System;
using System.IO;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PSI_JAM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int score = 0;
        Boolean flag;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            score = FirstCalculate() + SecondCalculate();
            if(score == FirstCalculate())
            {
                flag = true;
            }
            if(flag == true)
            {
                ClassResultTextBlock.Text = "Risk Class I";
                AdminRecommendationTextBlock.Text = "Outpatient Care";
            } else if(score <= 70)
            {
                ClassResultTextBlock.Text = "Risk Class II";
                AdminRecommendationTextBlock.Text = "Outpatient Care";
            } else if(score >70 && score < 91)
            {
                ClassResultTextBlock.Text = "Risk Class III";
                AdminRecommendationTextBlock.Text = "Outpatient or Observation Admission";
            } else if(score > 90 && score < 131)
            {
                ClassResultTextBlock.Text = "Risk Class IV";
                AdminRecommendationTextBlock.Text = "Inpatient Admission";
            } else if(score > 130)
            {
                ClassResultTextBlock.Text = "Risk Class V";
                AdminRecommendationTextBlock.Text = "Inpatient Admission (check for sepsis)";
            }
            Filer();
        }

        private int FirstCalculate()
        {
            int Value = 0;
            Value += Convert.ToInt32(AgeTextBox.Text);
            if (SexComboBox.SelectedIndex == 1)
            {
                Value -= 10;
            }
            if (NursingHomeResidentComboBox.SelectedIndex == 0)
            {
                Value += 10;
            }
            if (PleuralEffusOnXRayComboBox.SelectedIndex == 0)
            {
                Value += 10;
            }
            return Value;
        }

        private int SecondCalculate()
        {
            int Value = 0;

            if (Val(RespiratoryRateTextBox.Text)>= 30)
            {
                Value += 20;
            }
            if(Val(SystolicBloodPressureTextBox.Text) < 90)
            {
                Value += 20;
            }
            if ((PartialPressureOfOxygenRadioButton0.IsChecked ==true && Val(PartialPressureOfOxygenTextBox.Text) < 60) || (PartialPressureOfOxygenRadioButton1.IsChecked == true && Val(PartialPressureOfOxygenTextBox.Text) < 8))
            {
                Value += 10;
            }
            if ((TemperatureRadioButton0.IsChecked == true && (Val(TemperatureTextBox.Text) < 35 || Val(TemperatureTextBox.Text) > 39.9)) || (TemperatureRadioButton1.IsChecked == true && (Val(TemperatureTextBox.Text) < 95 || Val(TemperatureTextBox.Text) > 103.8)))
            {
                Value += 15;
            }
            if (Val(PulseTextBox.Text) >= 125)
            {
                Value += 10;
            }
            if (Val(pHTextBox.Text) < 7.35)
            {
                Value += 30;
            }
            if ((BUNRadioButton0.IsChecked == true && Val(BUNTextBox.Text) >=30) || (BUNRadioButton1.IsChecked == true && Val(BUNTextBox.Text) >=11))
            {
                Value += 20;
            }
            if (Val(SodiumTextBox.Text) < 130)
            {
                Value += 20;
            }
            if ((GlucoseRadioButton0.IsChecked == true && Val(GlucoseTextBox.Text) >=250) || (GlucoseRadioButton1.IsChecked == true && Val(GlucoseTextBox.Text) >= 14))
            {
                Value += 10;
            }
            if (Val(HematocritTextBox.Text) < 30)
            {
                Value += 10;
            }
            if (NeoPlasticDiseaseCancerComboBox.SelectedIndex == 0)
            {
                Value += 30;
            }
            if (LiverDiseaseComboBox.SelectedIndex == 0)
            {
                Value += 20;
            }
            if (CongestiveHeartFailureComboBox.SelectedIndex == 0)
            {
                Value += 20;
            }
            if (CerebrovascularDiseaseComboBox.SelectedIndex == 0)
            {
                Value += 10;
            }
            if (RenalDiseaseComboBox.SelectedIndex == 0)
            {
                Value += 10;
            }
            if (AlteredMentalStatusComboBox.SelectedIndex == 0)
            {
                Value += 20;
            }
            return Value;
        }

        private double Val(string s)
        {
            double result = Convert.ToDouble(s);
            return result;
        }

        private void Filer()
        {
            int count = 0;

            try
            {
                StreamReader File = new StreamReader("data.csv");
             
                string line;
                while ((line = File.ReadLine()) != null)
                {
                    count++;
                }

                File.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            try
            {
                StreamWriter File = new StreamWriter("data.csv", true);
                count += 1;
                string Age = AgeTextBox.Text;
                string Sex = SexComboBox.Text;

                int NHR = 999;
                if (NursingHomeResidentComboBox.SelectedIndex == 0)
                {
                    NHR = 1;
                }
                else if (NursingHomeResidentComboBox.SelectedIndex == 1)
                {
                    NHR = 0;
                }
                int NDC = 999;
                if (NeoPlasticDiseaseCancerComboBox.SelectedIndex == 0)
                {
                    NDC = 1;
                }
                else if (NeoPlasticDiseaseCancerComboBox.SelectedIndex == 1)
                {
                    NDC = 0;
                }
                int LD = 999;
                if (LiverDiseaseComboBox.SelectedIndex == 0)
                {
                    LD = 1;
                }
                else if (LiverDiseaseComboBox.SelectedIndex == 1)
                {
                    LD = 0;
                }
                int CHF = 999;
                if (CongestiveHeartFailureComboBox.SelectedIndex == 0)
                {
                    CHF = 1;
                }
                else if (CongestiveHeartFailureComboBox.SelectedIndex == 1)
                {
                    CHF = 0;
                }
                int CD = 999;
                if (CerebrovascularDiseaseComboBox.SelectedIndex == 0)
                {
                    CD = 1;
                }
                else if (CerebrovascularDiseaseComboBox.SelectedIndex == 1)
                {
                    CD = 0;
                }
                int RD = 999;
                if (RenalDiseaseComboBox.SelectedIndex == 0)
                {
                    RD = 1;
                }
                else if (RenalDiseaseComboBox.SelectedIndex == 1)
                {
                    RD = 0;
                }
                int AMS = 999;
                if (AlteredMentalStatusComboBox.SelectedIndex == 0)
                {
                    AMS = 1;
                }
                else if (AlteredMentalStatusComboBox.SelectedIndex == 1)
                {
                    AMS = 0;
                }
                int PEOX = 999;
                if (PleuralEffusOnXRayComboBox.SelectedIndex == 0)
                {
                    PEOX = 1;
                }
                else if (PleuralEffusOnXRayComboBox.SelectedIndex == 1)
                {
                    PEOX = 0;
                }

                string RR = RespiratoryRateTextBox.Text;
                string SBP = SystolicBloodPressureTextBox.Text;

                string Temp ="Missing";
                if (TemperatureRadioButton1.IsChecked == true)
                {
                    Temp = FTOC().ToString();
                }
                else
                {
                    Temp = TemperatureTextBox.Text;
                }

                string Pulse = PulseTextBox.Text;
                string PH = pHTextBox.Text;

                string BUN = "Missing";
                if (BUNRadioButton1.IsChecked == true)
                {
                    BUN = MMOLLTOMGDLBUN().ToString();
                }
                else
                {
                    BUN = BUNTextBox.Text;
                }


                string Sodium = SodiumTextBox.Text;

                string Glucose = "Missing";
                if (GlucoseRadioButton1.IsChecked == true)
                {
                    Glucose = MMOLLTOMGDLGLU().ToString();
                }
                else
                {
                    Glucose = GlucoseTextBox.Text;
                }

                string Hematocrit = HematocritTextBox.Text;

                string PPO = "Missing";
                if(PartialPressureOfOxygenRadioButton1.IsChecked == true)
                {
                    PPO=KPATOMMHG().ToString();
                }
                else
                {
                    PPO = PartialPressureOfOxygenTextBox.Text;
                }


                File.WriteLine("ID: " + count + ", Age: " + Age + ", Sex: " + Sex + ", Nurshing Home Resident: " + NHR + ", Neoplastic Disease (Cancer): " + NDC + ", Liver Disease: " + LD + ", Congestive Heart Failure: " + CHF + ", Cerebrovacular Disease: " + CD + ", Renal Disease: " + RD + ", Altered Mental Status: " + AMS + ", Pleural Effusion on X-Ray: " + PEOX + ", Respiratory Rate: " + RR + ", Systolic Blood Pressure: " + SBP + ", Temperature (in Celsius): " + Temp + ", Pulse: " + Pulse + ", pH: " + PH + ", BUN (in mg/dl): " + BUN + ", Sodium: " + Sodium + ", Glucose (in mg/dl): " + Glucose + ", Hermatocrit: " + Hematocrit + ", Partial Pressure of Oxygen (in mmHg): " + PPO);

                File.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }

        private double FTOC()
        {
            double celsius;
            double fahrenheit = Val(TemperatureTextBox.Text);
            celsius = (fahrenheit - 32) * 5 / 9;
            return celsius;
        }

        private double MMOLLTOMGDLBUN()
        {
            double mgdl;
            double mmoll = Val(BUNTextBox.Text);
            mgdl = mmoll * 18;
            return mgdl; 
        }

        private double MMOLLTOMGDLGLU()
        {
            double mgdl;
            double mmoll = Val(GlucoseTextBox.Text);
            mgdl = mmoll * 18;
            return mgdl;
        }

            private double KPATOMMHG()
        {
            double kPa = Val(PartialPressureOfOxygenTextBox.Text);
            double mmHg = kPa / 0.1333223684;
            return mmHg;

        }
    }
}
