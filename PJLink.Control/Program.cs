using PJLink.Client;

class Program
{
    private static PJLinkClient? _client;
    private static string? _currentHost;
    private static string? _currentPassword;
    private static bool _isAuthenticated;

    static async Task<int> Main()
    {
        Console.WriteLine("PJLink Control Application");
        Console.WriteLine("=========================");

        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Configure Connection");
            Console.WriteLine("2. Connect and Authenticate");
            if (_isAuthenticated)
            {
                Console.WriteLine("3. Power Control");
                Console.WriteLine("4. Input Control");
                Console.WriteLine("5. Audio/Video Mute Control");
                Console.WriteLine("6. Status Information");
                Console.WriteLine("7. Projector Information");
                Console.WriteLine("8. Exit");
                Console.Write("\nSelect an option (1-8): ");
            }
            else
            {
                Console.WriteLine("3. Exit");
                Console.Write("\nSelect an option (1-3): ");
            }

            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    ConfigureConnection();
                    break;
                case "2":
                    await ConnectAndAuthenticate();
                    break;
                case "3":
                    if (_isAuthenticated)
                        await PowerControl();
                    else
                        return 0;
                    break;
                case "4" when _isAuthenticated:
                    await InputControl();
                    break;
                case "5" when _isAuthenticated:
                    await MuteControl();
                    break;
                case "6" when _isAuthenticated:
                    await ShowStatus();
                    break;
                case "7" when _isAuthenticated:
                    await ShowProjectorInfo();
                    break;
                case "8" when _isAuthenticated:
                    return 0;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static void ConfigureConnection()
    {
        Console.WriteLine("\nConnection Configuration");
        Console.WriteLine("=======================");
        
        Console.Write("Enter host (IP address or hostname): ");
        var host = Console.ReadLine()?.Trim();
        
        Console.Write("Enter password (max 32 characters): ");
        var password = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(host))
        {
            Console.WriteLine("Host cannot be empty.");
            return;
        }

        if (password?.Length > 32)
        {
            Console.WriteLine("Password must be 32 or fewer characters.");
            return;
        }

        _currentHost = host;
        _currentPassword = password;
        _isAuthenticated = false;
        Console.WriteLine("Configuration saved.");
    }

    private static async Task ConnectAndAuthenticate()
    {
        if (string.IsNullOrEmpty(_currentHost) || _currentPassword == null)
        {
            Console.WriteLine("\nPlease configure the connection first (Option 1).");
            return;
        }

        try
        {
            _client?.Dispose();
            _client = new PJLinkClient(_currentHost, _currentPassword);
            Console.WriteLine($"\nConnecting to {_currentHost}...");
            
            _isAuthenticated = await _client.AuthenticateAsync();
            
            if (_isAuthenticated)
            {
                Console.WriteLine("Authentication successful!");
            }
            else
            {
                Console.WriteLine("Authentication failed - incorrect password or device not compatible.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            _isAuthenticated = false;
        }
    }

    private static async Task PowerControl()
    {
        if (_client == null)
        {
            Console.WriteLine("Not connected to a projector.");
            return;
        }

        try
        {
            var status = await _client.GetPowerStatusAsync();
            Console.WriteLine($"\nCurrent Power Status: {status}");

            Console.WriteLine("\nPower Control Menu:");
            Console.WriteLine("1. Power On");
            Console.WriteLine("2. Power Off");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("\nSelect an option (1-3): ");

            var choice = Console.ReadLine()?.Trim();
            PJLinkClient.PowerStatus newStatus = PJLinkClient.PowerStatus.StandBy;

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Powering on...");
                    newStatus = await _client.PowerOnAsync();
                    Console.WriteLine($"Command sent. Projector status: {newStatus}");
                    break;

                case "2":
                    Console.WriteLine("Powering off...");
                    newStatus = await _client.PowerOffAsync();
                    Console.WriteLine($"Command sent. Projector status: {newStatus}");
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            if (choice is "1" or "2")
            {
                await Task.Delay(2000);
                status = await _client.GetPowerStatusAsync();
                if (status != newStatus)
                {
                    Console.WriteLine($"Status changed to: {status}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task InputControl()
    {
        if (_client == null)
        {
            Console.WriteLine("Not connected to a projector.");
            return;
        }

        try
        {
            var currentInput = await _client.GetCurrentInputAsync();
            Console.WriteLine($"\nCurrent Input: {PJLinkClient.GetInputDescription(currentInput)}");

            // Try to get available inputs
            var availableInputs = await _client.GetAvailableInputsAsync();
            
            if (availableInputs == null)
            {
                // Device doesn't support input listing, fall back to showing all possible inputs
                Console.WriteLine("\nNote: This device doesn't support input discovery. Showing all possible inputs.");
                availableInputs = Enum.GetValues<PJLinkCommands.Input.Source>().ToList();
            }
            else if (availableInputs.Count == 0)
            {
                Console.WriteLine("\nNo inputs available on this device.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nInput Control Menu:");
            Console.WriteLine("Available inputs:");
            
            for (var i = 0; i < availableInputs.Count; i++)
            {
                var input = availableInputs[i];
                Console.WriteLine($"{i + 1}. {PJLinkClient.GetInputDescription(input)}");
            }
            
            Console.WriteLine($"{availableInputs.Count + 1}. Back to Main Menu");
            Console.Write($"\nSelect an option (1-{availableInputs.Count + 1}): ");

            var choice = Console.ReadLine()?.Trim();
            if (int.TryParse(choice, out var index) && index >= 1 && index <= availableInputs.Count)
            {
                var selectedInput = availableInputs[index - 1];
                Console.WriteLine($"Switching to {PJLinkClient.GetInputDescription(selectedInput)}...");
                var newInput = await _client.SetInputAsync(selectedInput);
                Console.WriteLine($"Input switched to: {PJLinkClient.GetInputDescription(newInput)}");
            }
            else if (choice == (availableInputs.Count + 1).ToString())
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task MuteControl()
    {
        if (_client == null)
        {
            Console.WriteLine("Not connected to a projector.");
            return;
        }

        try
        {
            var currentState = await _client.GetMuteStatusAsync();
            Console.WriteLine($"\nCurrent Mute State: {currentState}");

            Console.WriteLine("\nMute Control Menu:");
            Console.WriteLine("1. Video Mute On");
            Console.WriteLine("2. Video Mute Off");
            Console.WriteLine("3. Audio Mute On");
            Console.WriteLine("4. Audio Mute Off");
            Console.WriteLine("5. Audio/Video Mute On");
            Console.WriteLine("6. Audio/Video Mute Off");
            Console.WriteLine("7. Back to Main Menu");
            Console.Write("\nSelect an option (1-7): ");

            var choice = Console.ReadLine()?.Trim();
            PJLinkCommands.AudioVideoMute.MuteState? newState = choice switch
            {
                "1" => PJLinkCommands.AudioVideoMute.MuteState.VideoMuteOn,
                "2" => PJLinkCommands.AudioVideoMute.MuteState.VideoMuteOff,
                "3" => PJLinkCommands.AudioVideoMute.MuteState.AudioMuteOn,
                "4" => PJLinkCommands.AudioVideoMute.MuteState.AudioMuteOff,
                "5" => PJLinkCommands.AudioVideoMute.MuteState.AudioVideoMuteOn,
                "6" => PJLinkCommands.AudioVideoMute.MuteState.AudioVideoMuteOff,
                "7" => null,
                _ => throw new InvalidOperationException("Invalid option")
            };

            if (newState.HasValue)
            {
                var result = await _client.SetMuteAsync(newState.Value);
                Console.WriteLine($"Mute state changed to: {result}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task ShowStatus()
    {
        if (_client == null)
        {
            Console.WriteLine("Not connected to a projector.");
            return;
        }

        try
        {
            Console.WriteLine("\nProjector Status Information");
            Console.WriteLine("===========================");

            var errorStatus = await _client.GetErrorStatusAsync();
            Console.WriteLine($"\nError Status:\n{errorStatus}");

            var lampInfo = await _client.GetLampInfoAsync();
            Console.WriteLine($"\nLamp Information:\n{lampInfo}");

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task ShowProjectorInfo()
    {
        if (_client == null)
        {
            Console.WriteLine("Not connected to a projector.");
            return;
        }

        try
        {
            Console.WriteLine("\nProjector Information");
            Console.WriteLine("====================");

            var name = await _client.GetProjectorNameAsync();
            Console.WriteLine($"\nProjector Name: {name}");

            var manufacturer = await _client.GetManufacturerNameAsync();
            Console.WriteLine($"Manufacturer: {manufacturer}");

            var product = await _client.GetProductNameAsync();
            Console.WriteLine($"Product Name: {product}");

            var other = await _client.GetOtherInfoAsync();
            Console.WriteLine($"Other Information: {other}");

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
