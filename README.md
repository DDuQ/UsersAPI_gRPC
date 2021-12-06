
# UsersAPI_gRPC

REST API example that uses gRPC(syntax proto3) with the HTTP 1.1 protocol, C# 8.0 and .NET Core 3.1 


## Table of contents

- [Introduction](#introduction)

- [Differences between REST and gRPC](#differences-between-rest-and-grpc)

- [API Reference](#api-reference)

## Introduction

**gRPC** (Google Remote Procedure Call) is a variant of the RCP architecture, which implements API-driven RCP, using the HTTP 2.0 protocol and protocol buffers to define a service's interface and message structure. 

The reason for gRPC is to make communication between microservices faster.

**REST** aims to expose client services through the JSON format following the HTTP protocol. 

Here is a more detailed article about gRPC and REST APIs. 
[Click here](https://www.imaginarycloud.com/blog/grpc-vs-rest/)


## Differences between REST and gRPC

| REST | gRPC  | 
| :----------------------- | :------- | 
| JSON - XML (or alike) formats, easy reading comprehension for the programmer. |Message format (Proto buffers), programming language agnostic. |
| HTTP/1.1 - HTTP/2.0 protocol     | HTTP/2.0 protocol  | 
| Request-response communication model; communication slows down as the number of requests increases.| Client-response communication model; supports bidirectional communication and streaming which allows you to simultaneously receive multiple requests from different clients.|
| It only has **unary** interactions (request-response). | In addition to unary interactions, there are different types of streaming: **Server-streaming**, **Customer-streaming**, **Bidirectional-streaming** |



## API Reference 

#### Get all users

~~~
  GET /api/users
~~~
Returns a list of the users found in the database.

#### Get user

~~~
  GET /api/users/${id}
~~~

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. document id of an user  |

Returns either an user or a failed response if the user was not found.
#### Post user
~~~
  POST /api/users/${documentId}
~~~

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. document id of an user  |

Returns a response which notifies if the user was successfully added.

#### Update user
~~~
  PUT /api/users/${documentId}
~~~

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `documentId`      | `string` | **Required**. documentId of an user  |



Returns a response which notifies if the update was successful.

#### Delete user
~~~
  DELETE /api/users/${documentId}
~~~

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `documentId`      | `string` | **Required**. documentId of an user  |



Returns a response which notifies if the user deletion was successful.
