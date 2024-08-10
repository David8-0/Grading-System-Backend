# GradingSystem Admin Panel

This project is a grading system built with Angular 18 for the frontend and .NET Core Web API for the backend. The system allows an admin to manage student records and their grades across multiple subjects. The admin can add, edit, and search for students by various criteria. The portal is fully responsive, ensuring seamless user experience across different devices.

## Features

- **Student Management**
  - Add new students.
  - Edit existing student information.
  
- **Grade Management**
  - Add grades for students in multiple subjects.
  - Edit existing grades.

- **Search Functionality**
  - Search for students by name, grade, national ID, or academic year.

## Technologies Used

### Frontend

- **Angular 18**
  - The latest version of Angular.
  
- **Reactive Forms**
  - Utilized for handling form input and validation, providing a robust way to manage forms and form states.

- **Bootstrap**
  - For styling and UI components.

### Backend

- **.NET Core Web API**
  - Backend service handling all the business logic and data processing.

- **Entity Framework Core (EF Core)**
  - ORM (Object-Relational Mapper) for database access, providing efficient data manipulation and retrieval.

- **LINQ**
  - Used for querying the database in a more readable and concise manner.

- **Repository Pattern**
  - Abstracts the data layer, promoting a cleaner and more maintainable codebase.

- **Unit of Work Pattern**
  - Manages transactions across multiple repositories for consistency.

- **Data Transfer Objects (DTOs)**
  - Simplifies data exchange between the client and server.

- **SQL Server**
  - The database used for storing student and grade information.
  
## Getting Started
 ### Prerequisites

- Angular CLI
- .NET SDK
- SQL Server
    
**Installation**
1. Clone the repository:
   ```bash
   git clone https://github.com/David8-0/Grading-System-Frontend.git
   git clone https://github.com/David8-0/Grading-System-Backend.git
2. Install the dependencies for Angular Project:
    ```bash
     npm install
     ng s -o
3. Navigate to the `Grading-System-Backend` directory.
4. Set up your SQL Server connection string in `appsettings.json`.
5. Run the migrations to set up the database:
   ```bash
   dotnet ef database update
   dotnet run
## Usage

### Adding a Student

1. Navigate to the "Add Student" section.
2. Fill in the required fields such as Name, National ID, and Academic Year.
3. Click on the "Save" button to add the student.

### Editing a Student

1. Search for the student using the search functionality.
2. Click on the "Edit" button next to the student's record.
3. Update the necessary fields and click "Save."

### Adding or Editing Grades

1. Search for the student whose grades you want to manage in Subjects page.
2. Select Edit and change the values or Add to add new student.
3. Click "Save" to update the student's grades or add new student grades.

### Searching for Students

1. Use the search bar to search by name, grade, national ID, or academic year.
2. The list of students will be filtered based on your input.

   ## Contributing

We welcome contributions to improve the Student Manager Application. To contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch.
3. Make your changes and commit them.
4. Push to your fork and submit a pull request.


## Contact

For any inquiries, please contact:

- **Name**: David Ayad
- **Email**: [davidayad88@gmail.com](mailto:davidayad88@gmail.com)
- **GitHub**: [David Ayad](https://github.com/David8-0)


    
