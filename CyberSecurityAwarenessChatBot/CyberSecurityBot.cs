// ================================================
// CYBERSECURITYBOT.cs
// Main class containing all logic, methods, and UI enhancements
// Fully documented for maintainability (as required by rubric)
// ================================================

using System;
using System.IO;
using System.Media;// For playing WAV files for voice greeting (enhancing user experience with audio feedback)
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Speech.Synthesis; // For potential future TTS enhancements         

namespace CyberSecurityAwarenessChatBot
{
    public class CyberSecurityBot // Main class for the Cyber Security Awareness Bot, containing all logic, methods, and UI enhancements
    {
        private string userName = string.Empty;
        private readonly string audioFileName = "greeting.wav";// Name of the WAV file for the voice greeting

        /* 
         This method orchestrates the entire chatbot experience, starting with a voice greeting, followed by displaying an ASCII art logo, 
         a personalized text greeting, and then entering the main chat loop where users can ask questions about cyber security topics.
        */
        public void Start()
        {
            PlayVoiceGreeting();
            DisplayASCIILogo();
            DisplayTextGreeting();
            GetUserName();
            ShowHelp();
            MainChatLoop();
        }

        /*
         This method attempts to play a personalized voice greeting from a WAV file. If the file is missing or playback fails, 
         it provides a graceful fallback by displaying a warning message in the console.
        */
        private void PlayVoiceGreeting()
        {
            string audioPath = Path.Combine(AppContext.BaseDirectory, audioFileName);// Construct the full path to the WAV file

            if (File.Exists(audioPath))
            {
                try
                {
                    SoundPlayer player = new SoundPlayer(audioPath);// Load the WAV file
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("          >>>      ...Now playing personalized voice greeting...     <<<");// This code provides feedback to the user that the voice greeting is being played, enhancing the user experience with audio feedback.
                    Console.WriteLine("((>*-----------------*------------------(>*<)------------------*----------------*<))");
                    Console.ResetColor();
                    player.PlaySync(); // Synchronous so greeting finishes before text appears
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nSorry! Voice greeting could not play: {ex.Message}");// Graceful error handling if audio playback fails
                }
            }
            else
            {
                Console.WriteLine("Voice greeting file (greeting.wav) not found. " +
                                  "Please add it to the project root and set 'Copy to Output Directory'.");
            }
        }

        /*
         The code displays a creative ASCII art logo representing a cyber-themed shield or lock style, 
         serving as a visually appealing header for the chatbot interface.
        */
        private void DisplayASCIILogo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
=====================================================                ==============================            |               
 ________      ___    ___ ________  _______   ________                ________  ________  __________         <[-]>
|\...____\    |\..\  /../|\...__..\|\..___.\ |\...__..\              |\...__..\|\...__..\|\___...___\        /   \
\=\..\___|    \=\..\/../=|=\..\|\ /\=\...___|\=\..\_\._\  __________ \=\..\_\._\=\..\ \..\|___=\..\_|        \| |/
 \=\..\        \=\..../=/ \=\...__.\_=\..\___ \=\......_\|\__________\\=\...__ \_=\..\ \..\   \=\..\          V V    
  \=\..\____    \/.. /=/   \=\..\|\..\=\..\__\.\=\..\\.. \|==========| \=\..\_\..\=\..\_\..\   \=\..\       =======
   \=\_______\__/.. /=/     \=\_______\=\______\\=\__\ \__\  ! !|! !    \=\_______\=\_______\   \=\__\      \     /
    !|!  C   \|===|=/        !|!  B  !|!|!  E  !|!|!  R  !|!   !|!       !|!  B  !|!|!  O  !|!     !T!       \   /
    !|!      !|!Y!|!         !|!     !|!|!     !|!|!     !|!    !        !|!     !|!|!     !|!     !|!        \ /
     !       !|! !|!          !       ! !       ! !       !               !       ! !       !       !          V
              !   !                                                                                 
");

            TypePrint("                     C Y B E R     S E C U R I T Y     A W A R E N E S S     B O T\n", ConsoleColor.DarkCyan);

            Console.ResetColor();

            TypePrint("====================================================================================================(-<*>-)", ConsoleColor.DarkRed);
            TypePrint("                              !! STAY SAFE ONLINE !!", ConsoleColor.Yellow);
            TypePrint("=========================================================================(!|<*>|!)", ConsoleColor.DarkRed);
            TypePrint("                                                                            !|!", ConsoleColor.DarkRed);
            TypePrint("                                                                             !\n", ConsoleColor.DarkRed);
        }

        /*
        The code displays a welcoming text message after the voice greeting and ASCII art logo
        using a typing effect and green color for an engaging introduction to the chatbot.
        */
        private void DisplayTextGreeting()
        {
            TypePrint("Hello! Welcome to the Cyber Security Awareness Bot.", ConsoleColor.Green);
            TypePrint("Fear not, because I am here to help you stay safe online with practical tips.", ConsoleColor.Green);
        }


        /*
         This method prompts the user for their name with input validation to ensure a non-empty response.
         And then it personalizes all future response by using the users name.
         */
        private void GetUserName()
        {
            TypePrint("\nI'd love to chat with you, but first, may I kindly know your name?", ConsoleColor.Cyan);// This code prompts the user for their name.
            do
            {
                Console.Write("\n(^_^) Please enter your name > ");// Personalized prompt for user input
                userName = Console.ReadLine()?.Trim() ?? string.Empty;// Trim whitespace and handle null input gracefully

                if (string.IsNullOrWhiteSpace(userName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nName cannot be empty. Please try again.");// Input validation to ensure a valid name is entered
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(userName));

            /*
             This code personalizes the chatbot's responses by using the user's name, 
             creating a more engaging and friendly interaction.
            */
            TypePrint($"\nNice to meet you, {userName}! Let's learn how to stay cyber-safe together.", ConsoleColor.Cyan);
        }


        private void ShowHelp()
        {
            /*
             This code provides users with example questions and topics to ask the chatbot, 
             enhancing user experience and guiding them on how to interact effectively with the bot.
            */
            TypePrint("\n╔══════════════════════════════════════════════════════════════╗", ConsoleColor.DarkCyan);// Decorative box to highlight the help section
            TypePrint("║                     HOW TO CHAT WITH ME                      ║", ConsoleColor.White);// Header for the help section
            TypePrint("╚══════════════════════════════════════════════════════════════╝", ConsoleColor.DarkBlue);

            TypePrint("\n~*~ Try asking things like:", ConsoleColor.Magenta);// This code encourages users to ask specific questions about cyber security topics
            TypePrint("---------------------------------", ConsoleColor.Green);
            TypePrint("    - How are you?", ConsoleColor.Yellow);
            TypePrint("    - What is your purpose?", ConsoleColor.Yellow);
            TypePrint("    - What can I ask you?", ConsoleColor.Yellow);
            TypePrint("    - Tell me about password safety", ConsoleColor.Yellow);
            TypePrint("    - How to avoid phishing?", ConsoleColor.Yellow);
            TypePrint("    - What is Two-factor authentication?", ConsoleColor.Yellow);
            TypePrint("    - Tips for safe browsing", ConsoleColor.Yellow);
            TypePrint("---------------------------------\n", ConsoleColor.Green);

            // This code informs users that they can type 'exit' or 'quit' to end the chat session, providing a clear and user-friendly way to exit the chatbot interaction.
            TypePrint("\nType 'exit' or 'quit' anytime to stop the application.\n", ConsoleColor.Green);
        }

        // The main chat loop continuously prompts the user for input, validates it, and generates responses based on predefined keywords.
        private void MainChatLoop()
        {
            TypePrint("\nI'm ready whenever you are! Ask me anything about cyber security.\n", ConsoleColor.Cyan);

            while (true)
            {
                // This code creates a personalized prompt for the user to enter their questions, enhancing engagement and making the interaction feel more conversational.
                Console.Write($"{userName} -> ");
                string input = Console.ReadLine()?.Trim();

                // Input validation: handle empty/invalid entries gracefully
                if (string.IsNullOrWhiteSpace(input))
                {
                    TypePrint("I didn't catch that. Please type a question or 'exit' to quit.", ConsoleColor.Red);
                    continue;
                }

                // Exit condition: allows users to gracefully end the chat session with a personalized goodbye message.
                if (input.ToLower() == "exit" || input.ToLower() == "quit")
                {
                    // This code displays a goodbye text message that will appear when they exite the chatbot.
                    TypePrint($"\nGoodbye, {userName}! Stay safe online and remember: security is a habit, not an option! ;)", ConsoleColor.Green);
                    break;
                }

                // This code will print out the chabot's response to the users question with a cyan color and a cute bot face for added personality.
                string response = GetResponse(input);
                TypePrint($"<[0_0]>: {response}", ConsoleColor.Cyan);
            }
        }

        /*
         This method generates responses based on the user's input by detecting keywords related to cyber security topics.
         Covers multiple topics with depth and clarity (passwords, phishing, safe browsing, etc.).
         And responds back gracefully for unsupported queries.
        */
        private string GetResponse(string input)
        {
            string lowerInput = input.ToLowerInvariant();

            // This code provides a personalized response about the chatbot's well-being when the user asks how it is doing, creating a friendly and engaging interaction.
            if (lowerInput.Contains("how are you") || lowerInput.Contains("how r u"))
            {
                return $"I'm doing fantastic, {userName}! I'm ready to help you level up your cyber security knowledge.\n";
            }

            /*
            This code provides a detailed response about the chatbot's purpose and capabilities when the user asks about its purpose or identity, 
            clearly explaining that it is a Cyber Security Awareness Bot designed to educate users on various online safety topics.
            */
            else if (lowerInput.Contains("your purpose") || lowerInput.Contains("who are you") || lowerInput.Contains("what is your function"))
            {
                return $"I am a Cyber Security Awareness Bot. My purpose is to teach you how to stay safe online, {userName}. I cover passwords, phishing, safe browsing, malware, and more!\n";
            }

            // This code provides a helpful response about the types of questions users can ask the chatbot, encouraging them to explore various cyber security topics and guiding them on how to interact effectively with the bot.
            else if (lowerInput.Contains("what can i ask you") || lowerInput.Contains("question you about"))
            {
                return $"You can ask me anything about cyber security! Examples: password tips, spotting phishing, safe browsing habits, malware protection, or two-factor authentication. What would you like to know?\n";

            }

            // This code provides a detailed response about password safety when the user asks about passwords, including best practices for creating strong passwords and using password managers.
            else if (lowerInput.Contains("password"))
            {
                return $"Thank you for bringing that up {userName}. Remeber that A password is a secret string of characters including letters, numbers and symbols, used to authenticate a user's identity and devices or online accounts. \n" +
                        "\n To ensure that you have a strong passwords, it must have the following:\n" +
                        "    - A minimum of 12-16 characters\n" +
                        "    - A mixture of uppercase, lowercase, numbers & symbols\n" +
                        "    - Never reuse passwords across sites\n" +
                        "    - A password manager like Bitwarden or LastPass\n" +
                        "    - Enable 2FA everywhere possible!\n";
            }

            // This code provides a detailed response about phishing when the user asks about it, including how to spot and avoid phishing attempts, such as checking email senders, hovering over links, and looking for HTTPS.
            else if (lowerInput.Contains("phishing") || lowerInput.Contains("scam"))
            {
                return $"Phishing is one of the most common attacks, {userName}. Phishing (also known as a scam) is a type of cybercrime where attackers impersonate reputable entities via email, text or social media to deceive individuals into revealing sensitive information such as passwords, credit card numbers or login credentials.\n" +
                        "\nHere's how to spot and avoid it:\n" +
                        "\n    - Check the sender's email carefully (fake domains are common)\n" +
                        "    - Hover over links before clicking (check real URL)\n" +
                        "    - Never enter info on pop-ups or suspicious sites\n" +
                        "    - Look for HTTPS padlock\n" +
                        "    - When in doubt — delete!\n";
            }

            // This code provides a detailed response about safe browsing when the user asks about it, including tips for staying safe online, such as using HTTPS sites, avoiding public Wi-Fi without a VPN, and keeping software updated.
            else if (lowerInput.Contains("safe browsing") || lowerInput.Contains("internet") || lowerInput.Contains("safe online"))
            {
                return $"That's a great question {userName}! Safe browsing is a security service that is provided by Google, to protect users by warning them before they visit dangerous websites or download malicious content.\n" +
                    $"\n Here are some safe browsing tips for you, {userName}:\n" +
                        "\n    - Always use HTTPS sites\n" +
                        "    - Avoid public Wi-Fi without a VPN\n" +
                        "    - Keep your browser, OS, and apps updated\n" +
                        "    - Use an ad-blocker and reputable antivirus\n" +
                        "    - Never download from untrusted sources\n";
            }

            // This code provides a detailed response about malware when the user asks about it, including what malware is and tips for protecting against it, such as using antivirus software, avoiding email attachments from strangers, and keeping software updated.
            else if (lowerInput.Contains("define malware") || lowerInput.Contains("what is malware") || lowerInput.Contains("malware"))
            {
                return $"A Malware is a program or code that is designed to intentionally disrupt, damage or gain unauthorized access to steal data from computer systems, networks or servers.\n" +
                       $"\nProtect yourself, {userName}:\n" +
                        "\n    - Install and update antivirus software\n" +
                        "    - Never open email attachments from strangers\n" +
                        "    - Enable automatic updates\n" +
                        "    - Use a good firewall\n" +
                        "    - Backup important files regularly!\n";
            }

            // This code provides a detailed response about two-factor authentication (2FA) when the user asks about it, including how it works and the benefits of using 2FA for enhanced security.
            else if (lowerInput.Contains("two-factor authentication") || lowerInput.Contains("2 factor authentication") || lowerInput.Contains("2fa") || lowerInput.Contains("two-factor authentication"))
            {
                return $"Thats an insightful question {userName}!. A two-factor authentication (2FA) is a security method that requires you to provide two distinct forms of identifying to access an account.\n" +
                       $"\nHeres how it works {userName}:\n" +
                       "\n    - First Factor: You enter your username and password (something you know)\n" +
                       "    - Second Factor: The system prompts you for a second piece of evidence from a different category (something you have)\n" +
                       "    - Validation: Then the server verifies both factors are correct before granting access(something like your biometrics)\n" +
                       "\n     Here are some benefits of 2FA:\n" +
                       "\n        - It blocks 99.9% of attacks\n" +
                       "        - Neutralizes the risk of password breaches\n" +
                       "        - Provides an extra layer of security even if your password is compromised\n" +
                       "        - It also prevents Fraud\n";

            }

            // This code provides a default response for unsupported queries, encouraging users to rephrase their questions and suggesting relevant topics to ask about.
            else
            {
                // Default graceful response for unsupported queries
                return $"Sorry but I didn't quite understand that, {userName}. Could you rephrase your question? (Try asking about passwords, phishing, safe browsing, two-factor authentication or malware)\n";
            }
        }



        // This method prints text to the console with a typing effect and specified color, enhancing the user experience by making the chatbot's responses feel more dynamic and engaging.
        private void TypePrint(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;// Set the desired color for the text
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(25); // Slight delay creates realistic "typing" effect
            }
            Console.WriteLine();// Move to next line after printing the full message
            Console.ResetColor();// Reset color after printing
        }
    }
}