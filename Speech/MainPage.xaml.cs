using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Speech.Resources;
using Windows.Phone.Speech.Synthesis;
using Windows.ApplicationModel;
using System.Text;
using Windows.Phone.Speech.Recognition;

namespace Speech
{
    public partial class MainPage : PhoneApplicationPage
    {
        SpeechSynthesizer _synth=new SpeechSynthesizer();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void TextToSpeech_Click(object sender, RoutedEventArgs e)
        {
            await _synth.SpeakTextAsync("Windows Phone 8 Rocks!");
        }

        private async void TextToSpeechDifferentLanguage_Click(object sender, RoutedEventArgs e)
        {
            var frenchVoice=InstalledVoices.All
                                           .Where(voice => voice.Language.Equals("fr-FR") & voice.Gender == VoiceGender.Female)
                                           .FirstOrDefault();
            _synth.SetVoice(frenchVoice);

            await _synth.SpeakTextAsync("Salut tout le monde!");
        }

        private async void SSMLString_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb=new StringBuilder();
            sb.Append("<speak version=\"1.0\" ");
            sb.Append("xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"en-US\">");
            sb.Append(" Windows Phone 8 speaks from SSML! </speak>");

            await _synth.SpeakSsmlAsync(sb.ToString());
        }

        private async void SSMLFromFile_Click(object sender, RoutedEventArgs e)
        {
            string path=Package.Current.InstalledLocation.Path + "\\SSMLFromFile.ssml";
            Uri changeVoice=new Uri(path, UriKind.Absolute);
            await _synth.SpeakSsmlFromUriAsync(changeVoice);
        }

        private async void btnSpeechToTextBasicImplementation_Click(object sender, RoutedEventArgs e)
        {
            SpeechRecognizerUI recoWithUI=new SpeechRecognizerUI();
            SpeechRecognitionUIResult recoResult=await recoWithUI.RecognizeWithUIAsync();
            MessageBox.Show(string.Format("You said {0}.", recoResult.RecognitionResult.Text));
        }

        private async void btnSpeechToText_Click_1(object sender, RoutedEventArgs e)
        {
            SpeechRecognizerUI recognizerUI=new SpeechRecognizerUI();
            recognizerUI.Settings.ListenText="I'm listening...";
            recognizerUI.Settings.ExampleText="Speech recognition rocks on Windows Phone!";

            var speechRecognitionUIResult=await recognizerUI.RecognizeWithUIAsync();

            if (speechRecognitionUIResult.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
            {
                MessageBox.Show(string.Format("You said {0}.", speechRecognitionUIResult.RecognitionResult.Text));
            }
        }

        private async void btnSpeechToTextwithGrammar_Click(object sender, RoutedEventArgs e)
        {
            SpeechRecognizerUI speechRecognizer=new SpeechRecognizerUI();
            speechRecognizer.Settings.ListenText="Navigate Forwards or Backwards?";
            speechRecognizer.Settings.ExampleText="next, previous";

            speechRecognizer.Settings.ReadoutEnabled=true;
            speechRecognizer.Settings.ShowConfirmation=true;

            speechRecognizer.Recognizer.Grammars.AddGrammarFromList("answer",
                new string[]
                {
                    "next",
                    "previous"
                });

            SpeechRecognitionUIResult result=await speechRecognizer.RecognizeWithUIAsync();

            if (result.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
            {
                MessageBox.Show(string.Format("You said {0}.", result.RecognitionResult.Text));
            }
        }
        
    }
}