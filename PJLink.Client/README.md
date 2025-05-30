# PJLink.Client

A .NET client implementation of the PJLink protocol for projector control. This client specifically implements PJLink Class 1 specification, which is the most widely supported version of the protocol.

## About PJLink Class 1

PJLink is a unified standard for operating and controlling data projectors and displays. Class 1 is the base specification that provides essential control features:

- Power state control and monitoring
- Input source switching and status
- Audio/Video mute control
- Error and lamp status monitoring
- Device information queries

For more information about the PJLink specification, visit [PJLink Official Website](https://pjlink.jbmia.or.jp/english/).

## Features

- Full implementation of PJLink Class 1 protocol
- Proper connection handling with automatic disposal
- Support for all Class 1 commands:
  - Power control (on/off/status)
  - Input selection (RGB, Video, Digital, Storage, Network)
  - Audio/Video mute control
  - Error status monitoring
  - Lamp information
  - Projector information (name, manufacturer, model)
- Comprehensive input source definitions following official guidelines
- Proper authentication handling for secure devices
- Automatic connection management per command
- Full async support with cancellation

## Installation

```shell
dotnet add package PJLink.Client
```

## Usage

```csharp
using PJLink.Client;

// Create a client instance
using var client = new PJLinkClient("192.168.1.100", "password");

// Authenticate
var isAuthenticated = await client.AuthenticateAsync();
if (!isAuthenticated)
{
    Console.WriteLine("Authentication failed");
    return;
}

// Power on the projector
var status = await client.PowerOnAsync();
Console.WriteLine($"Power status: {status}");

// Switch to HDMI input
var input = await client.SetInputAsync(PJLinkCommands.Input.Source.Digital_Hdmi);
Console.WriteLine($"Current input: {input}");

// Get error status
var errorStatus = await client.GetErrorStatusAsync();
Console.WriteLine($"Error status: {errorStatus}");
```

## Input Sources

The client supports all standard input sources defined in the PJLink Class 1 specification:

### RGB (11-19)
- D-SUB
- 5 BNC
- DVI-I Analog
- SCART
- M1-DA

### Video (21-29)
- Composite RCA
- Component (3 RCA)
- Component BNC
- S-Video
- D Terminal

### Digital (31-39)
- DVI-I Digital
- DVI-D
- HDMI
- SDI
- DisplayPort
- Wireless HDMI

### Storage (41-49)
- USB Type A
- PC Card
- CompactFlash
- SD Card

### Network (51-59)
- Wired LAN (RJ-45)
- Wireless LAN
- USB Type B
- Wireless USB
- Bluetooth

## Documentation

Each command follows the PJLink Class 1 protocol specification and properly handles the connection lifecycle:

- A new TCP connection is established for each command
- The connection is automatically closed after receiving the response
- Authentication is handled automatically when required
- All commands are async and support cancellation

## License

This project is licensed under the MIT License - see the LICENSE file for details. 