# File Sanitizer

File Sanitizer is a web application that allows you to sanitize files based on their format. It supports two file formats: ABC and EFG. Malicious files with invalid block sequences are sanitized by replacing the bad blocks with a predefined block sequence.

## Features

- Accepts files in ABC and EFG formats for sanitization.
- Validates the file format before sanitization.
- Sanitizes malicious files by replacing invalid blocks.
- Provides a downloadable sanitized file as the output.
- Offers a user-friendly interface to interact with the application using Swagger.

## Requirements

- .NET Core 3.1 or higher
- Compatible web browser

## Installation

1. Clone the repository or download the source code.
2. Build the project using the appropriate .NET Core build commands.
3. Run the application using the appropriate .NET Core run commands.
4. Access the application in your web browser at the specified URL.

Docker Installation: 

1. Clone the repository or download the source code.
2. Navigate to the root directory of the project.

## Building and Running the Docker Container

1. Open a terminal or command prompt.
2. Build the Docker image using the following command:

   docker build -t filesanitizer .
   
3. Run the Docker container using the following command:

   docker run -p filesanitizer


## Usage

1. Launch the application Swagger in your web browser http://localhost:[portNumber]/swagger/index.html
2. The Swagger UI page will be displayed, showing the available endpoints and operations.
3. Expand the "FileSanitizer" section to view the available operations.
4. Click on the "POST /filesanitizer/sanitize" endpoint to expand it.
5. Click the "Try it out" button.
6. Select a file in either ABC or EFG format that you want to sanitize by clicking the "Choose File" button and selecting the file from your local machine.
7. Click the "Execute" button to initiate the sanitization process.
8. If the file format is valid, the application will sanitize the file and provide a downloadable link to the sanitized file.
9. If the file format is invalid, an error message will be displayed indicating the invalid format.

