# EduLearn

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 19.2.15.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

##  **Where and How Bindings Are Used**

### **1. Property Binding**
- **Where:** In `app.component.html`
- **How:**  
  ```html
  <app-course-detail [course]="selectedCourse"></app-course-detail>
  ```
  - The `[course]` binds the selected course object from `AppComponent` to the `CourseDetailComponent`.
  - This allows `CourseDetailComponent` to display the details of the course that was clicked in the list.

---

### **2. Event Binding**
- **Where:** In `app.component.html` and `course-list.component.html`
- **How:**  
  ```html
  <app-course-list (courseSelected)="selectedCourse = $event"></app-course-list>
  ```
  - `(courseSelected)` is an event emitted from `CourseListComponent` when the "View Details" button is clicked.
  - The `$event` contains the clicked course object and is assigned to `selectedCourse`.

---

### **3. Two-Way Binding**
- **Where:** In `course-detail.component.html`
- **How:**  
  ```html
  <input [(ngModel)]="course.title">
  <textarea [(ngModel)]="course.description"></textarea>
  ```
  - `[(ngModel)]` binds the form input values to the `course` object.
  - Any change in the input fields is instantly reflected in the course object, and vice versa.

---

##  **Features**
- Responsive UI with modern design.
- View all courses in a styled list.
- Select a course to see details instantly.
- Edit course title and description in real-time.
- Smooth hover effects and attractive design.


## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
