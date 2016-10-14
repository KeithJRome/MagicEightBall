using System;
using Xamarin.Forms;

namespace MagicEightBall.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GetNewAnswer(object sender, EventArgs e)
        {
            AskButton.IsEnabled = false;
            try
            {
                var proxy = new Oracle("http://magic-8-ball-api.azurewebsites.net");
                var response = await proxy.AskQuestion("The question really doesn't matter");
                AnswerLabel.Text = response.Response;
                switch (response.Disposition)
                {
                    case "negative":
                        AnswerLabel.TextColor = Color.Pink;
                        break;
                    case "neutral":
                        AnswerLabel.TextColor = Color.White;
                        break;
                    case "positive":
                        AnswerLabel.TextColor = Color.Lime;
                        break;
                }
            }
            finally
            {
                AskButton.IsEnabled = true;
            }
        }
    }
}
