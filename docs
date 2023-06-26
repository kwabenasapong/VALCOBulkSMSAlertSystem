## Setting up and Configuring VSMSAlert

To set up and configure VSMSAlert, please follow the instructions below:

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) installed on your machine.
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or a compatible database management system installed and running.
- API credentials from [Hubtel](https://www.hubtel.com/developers) to access the Quick SMS API.

### Installation Steps

1. Clone the repository:

   ```shell
   git clone https://github.com/kwabenasapong/VSMSAlert.git
   ```

2. Navigate to the project directory:

   ```shell
   cd VALCOSMSAlertSystem
   ```

3. Install the project dependencies:

   ```shell
   dotnet restore
   ```

4. Update the database connection string:

   - Open the `appsettings.json` file.
   - Locate the `ConnectionStrings` section.
   - Replace the placeholder values with your database connection details.

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-server-name;Database=your-database-name;User=your-username;Password=your-password;"
     }
     ```

5. Apply the database migrations:

   ```shell
   dotnet ef database update
   ```

   This will create the necessary tables in the database.

6. Update the Hubtel API credentials:

   - Open the `appsettings.json` file.
   - Locate the `HubtelSettings` section.
   - Replace the placeholder values with your Hubtel API credentials.

     ```json
     "HubtelSettings": {
       "ApiKey": "your-api-key",
       "ClientId": "your-client-id",
       "ClientSecret": "your-client-secret"
     }
     ```

7. Run the application:

   ```shell
   dotnet run
   ```

   The application will start running on `https://localhost:7071` by default.

8. Access the application:

   Open your web browser and navigate to `https://localhost:7071` to access the VSMSAlert application.

### Configuration

Additional configuration options can be found in the `appsettings.json` file. You can modify these settings based on your requirements. Some of the key configuration options include:

- `Logging`: Configure logging options such as log levels and output destinations.
- `EmailSettings`: Configure email server settings for email notifications.
- `TwilioSettings`: If you want to use Twilio instead of Hubtel for SMS messaging, provide your Twilio API credentials.
- `AdminSettings`: Configure the admin account details, such as the admin username and password.

### Documentation

For more detailed instructions on setting up, configuring, and using VSMSAlert, please refer to the [documentation](docs).

If you encounter any issues during the setup process or have any questions, please feel free to reach out to us or consult the documentation for troubleshooting steps.
