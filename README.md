# VSMSAlert

VSMSAlert is an ASP.NET Core MVC application designed for sending SMS messages. It provides a user-friendly interface that allows users to send SMS messages to either single or multiple recipients. The application utilizes the Hubtel Quick SMS API for seamless integration with the SMS gateway provider.

## Key Features

- User Management: The system includes user management functionality, allowing users to register and log in to the application. An administrator oversees non-admin accounts and handles user registration.

- SMS Sending: Users can send SMS messages independently using the application. The Hubtel Quick SMS API is utilized to send the messages to the recipients.

- Contact Management: The application provides a shared contact list where users can access and manage their contacts. This enables users to easily select recipients for their SMS messages.

- Administrator Control: The administrator has supervision over non-admin accounts and can manage user registrations. This ensures centralized management while providing decentralized control to individual users.

## Technologies Used

- ASP.NET Core MVC: The application is built using the ASP.NET Core MVC framework, which provides a robust and scalable architecture for web applications.

- C#: The backend of the application is developed using C#, a powerful and versatile programming language.

- Entity Framework Core: Microsoft's Entity Framework Core is used for database management, providing seamless integration with Microsoft SQL Server.

- Hubtel Quick SMS API: The Hubtel Quick SMS API is integrated into the application to enable sending SMS messages to recipients.

## Getting Started

To get started with VSMSAlert, follow these steps:

1. Clone the repository: `git clone https://github.com/your-username/VSMSAlert.git`
2. Install the necessary dependencies by running `dotnet restore` in the project directory.
3. Set up the database by updating the connection string in the `appsettings.json` file and running the database migrations using `dotnet ef database update`.
4. Obtain API credentials from Hubtel and update the configuration settings in the `appsettings.json` file.
5. Run the application using `dotnet run` command.

For detailed instructions on setting up and configuring the application, please refer to the [documentation](docs).

## Contributing

Contributions to VSMSAlert are welcome! If you find any bugs or want to suggest enhancements, please open an issue or submit a pull request. Make sure to follow the [contribution guidelines](CONTRIBUTING.md) when making any contributions.

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgements

We would like to thank the developers and contributors of the ASP.NET Core, C#, Entity Framework Core, and Hubtel Quick SMS API projects for their excellent tools and services that made this project possible.
