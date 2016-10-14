using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MagicEightBall.Mobile
{
    public partial class MainPage : ShakablePage
    {
        public MainPage()
        {
            InitializeComponent();

            HideAnswer();
        }

        public override void OnShake()
        {
            GetNewAnswer(null, EventArgs.Empty);
        }

        void HideAnswer()
        {
            CenterDie.Opacity = 0;
            AnswerLabel.Opacity = 0;
        }

        async Task ShowAnswer()
        {
            var anim1 = CenterDie.FadeTo(1, 750, Easing.CubicOut);
            var anim2 = AnswerLabel.FadeTo(1, 750, Easing.CubicOut);

            await Task.WhenAll(anim1, anim2);
        }

        public async void GetNewAnswer(object sender, EventArgs e)
        {
            AskButton.IsEnabled = false;
            HideAnswer();
            try
            {
                var proxy = new Oracle("http://magic-8-ball-api.azurewebsites.net");
                OracleResponse response = null;
                try
                {
                    response = await proxy.AskQuestion("The question really doesn't matter");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                    return;
                }
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
                await ShowAnswer();
            }
            finally
            {
                AskButton.IsEnabled = true;
            }
        }
    }
}
