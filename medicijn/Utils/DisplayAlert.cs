using System;
using Xamarin.Forms;

namespace medicijn.Utils
{
    public static class DisplayAlert
    {
        public static void PromptError(string message)
        {
            MessagingCenter.Send((App)Application.Current, "DisplayErrorAlert", message);
        }

        public static void PromptSuccess(string message)
        {
            MessagingCenter.Send((App)Application.Current, "DisplaySuccessAlert", message);
        }

        public static void PromptInfo(string message)
        {
            MessagingCenter.Send((App)Application.Current, "DisplaySuccessInfo", message);
        }

        public static void PromptMessageWithOptions(string message)
        {
            MessagingCenter.Send((App)Application.Current, "DisplayMessageWithOptions", message);
        }
    }
}
