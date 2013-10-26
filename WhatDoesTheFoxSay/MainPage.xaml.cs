using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace WhatDoesTheFoxSay
{
    public class PSound
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Sound { get; set; }
    }

    public partial class MainPage
    {
        private readonly PSound[] _sounds;
        private int _soundId;
        private readonly Random _rnd = new Random();

        public MainPage()
        {
            InitializeComponent();

            _sounds = new[]
            {
                new PSound {Sound = "ahee", Text1 = "A-hee-ahee", Text2 = "ha-hee!"},
                new PSound {Sound = "aooo", Text1 = "A-oo-oo", Text2 = "oo-ooo!"},
                new PSound {Sound = "chacha", Text1 = "Chacha-chacha", Text2 = "chacha-chow!"},
                new PSound {Sound = "fraka", Text1 = "Fraka-kaka-kaka", Text2 = "kaka-kow!"},
                new PSound {Sound = "gering", Text1 = "Gering-ding-ding", Text2 = "ding-dingeringeding!"},
                new PSound {Sound = "hatee", Text1 = "Hatee-hatee", Text2 = "hatee-ho!"},
                new PSound {Sound = "jofftchoff", Text1 = "Joff-tchoff-tchoffo", Text2 = "tchoffo-tchoff!"},
                new PSound {Sound = "wapa", Text1 = "Wa-pa-pa-pa", Text2 = "pa-pa-pow!"}
            };

            ContentPanel.Background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"Assets\bg.jpg", UriKind.Relative))
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Roll();
        }

        private void Roll()
        {
            _soundId = _rnd.Next(_sounds.Length);
            Text1.Text = _sounds[_soundId].Text1;
            Text2.Text = _sounds[_soundId].Text2;
            PlayCurrentSound();
        }

        private void PlaySound(string sound)
        {
            RollButton.Visibility = Visibility.Collapsed;
            var info = Application.GetResourceStream(new Uri(@"Assets\" + sound + ".wav", UriKind.Relative));
            var effect = SoundEffect.FromStream(info.Stream);
            FrameworkDispatcher.Update();
            effect.Play();

            var ev = new DispatcherTimer { Interval = effect.Duration };
            ev.Tick += ShowRollButton;
            ev.Start();
        }

        private void ShowRollButton(object sender, EventArgs args)
        {
            RollButton.Visibility = Visibility.Visible;
            var timer = sender as DispatcherTimer;
            if (timer != null)
                timer.Stop();
        }

        private void PlayCurrentSound()
        {
            PlaySound(_sounds[_soundId].Sound);
        }

        private void Image_Tap(object sender, GestureEventArgs e)
        {
            PlaySound("what_does_the_fox_say");
        }

        private void Text1_OnTap(object sender, GestureEventArgs e)
        {
            PlayCurrentSound();
        }

        private void Text2_OnTap(object sender, GestureEventArgs e)
        {
            PlayCurrentSound();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Roll();
        }
    }
}
