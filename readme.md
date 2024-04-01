# TRISTAR Assessment

TRISTAR Proprietary and Confidential - All Rights Reserved.    
Unauthorized copying of this file, via any medium is strictly prohibited.    
Proprietary and confidential.


## Introduction

This repository is designed to be a take-home assessment of software development candidates for [TRISTAR](https://www.star3.com). The assessment is designed to test your ability to understand line-of-business architecture and development practices. This solution is a simplified example of the kind of architecture that you are likely to find while working at TRISTAR.

## Architecture

This solution consists of a web server project that runs a very basic http API and a client project that contains a class that is designed to communicate with the http API. It also contains a core project that contains common code that is shared between the client and server projects, along with a test project where integration tests are contained.

The http API has basic endpoints for manipulating `Person` resources. With the API, you can create, read, update, and delete `Person` objects. To view the API capabilities and details, you can run the server project and visit [http://localhost:3000/api](http://localhost:3000/api). _Note that this link will not work unless you are running the server project!_

The following is a quick overview of each project:

* [TRISTAR.Assessment.Core](TRISTAR.Assessment.Core/readme.md): All files shared between client and server are found here. For example, all repository interfaces are defined here.
* [TRISTAR.Assessment.Client](TRISTAR.Assessment.Client/readme.md): Client-side files are found in this project. For example, the [client-side implementation](TRISTAR.Assessment.Client/People/PersonClientRepository.cs) of [IPersonRepository](TRISTAR.Assessment.Core/People/IPersonRepository.cs) lives here.
* [TRISTAR.Assessment.Server](TRISTAR.Assessment.Server/readme.md): This project contains the code for the http api. You can run this project and view the api at [http://localhost:3000/api](http://localhost:3000/api).
* [TRISTAR.Assessment.Tests](TRISTAR.Assessment.Tests/readme.md): Integration tests are found in this project.

## Assessment Instructions

1. Make a new git branch named `development` from `master`. Your changes will reside in this branch.
1. Implement the methods in [PersonClientRepository](TRISTAR.Assessment.Client/People/PersonClientRepository.cs).
1. Ensure all integration tests in [PersonIntegrationTests](TRISTAR.Assessment.Tests/PersonIntegrationTests.cs) pass. You are __not__ allowed to modify the tests.
1. Commit your changes to the `development` branch.
1. Merge your changes from the `development` branch into the `master` branch.
1. Put the repository into a zip file and send it back to your TRISTAR point of contact.

You are encouraged to add files or NuGet packages to the project as you see fit, especially if you think it will result in better code. This is your chance to show your skills, but keep in mind that there is elegance in simplicity!
