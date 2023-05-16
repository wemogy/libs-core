# Errors

The `Errors` namespace contains a collection of all error types, that can occur in an application.

## Motivation

The main idea behind this concept is to define a unified classification of errors, that ensures a consistent usage of exception types. Normally it could happen that one developer throws an exception of type `ArgumentException` if there is an argument invalid. Another developer may throws in this case an exception of type `ValidationException`. To prevent this problem, we have defined a uniform interface of error types, which specifies which exception type should be thrown for the corresponding error.

Another advantage of this approach is that it allows defining a unified middleware that can code the exceptions to the corresponding HTTP statuses and assign a proper description.

## Properties of an error

### Code

Sample: `auth-0001`

This property must contain a **unique** identifier for the error which is unique to our application. Generally, there is no convention for the error field, except that it be unique.

Usually, this field contains only alphanumerics and connecting characters, such as dashes or underscores. For example, 0001, auth-0001 and incorrect-user-pass are canonical examples of error codes.

This error code could be used as part of support links like: `https://example.com/help/error/auth-0001`

### Description

Sample: `Incorrect username and password`

This property should contain a brief human-readable description of the error.

### InnerException

Use this property to propagate the original exception if you caught one.
