# DC Assignment 2 Part A
 
You will be building a two-tier application called “Centre Booking”. Your client (maybe the
council or government or any other institution) manages a list of community centres. These
centres could be hired by general people to organise different activities, i.e., meetings and
parties. Your client wants to build the web application, so general people can book these
centres online. Please follow previous tutorials to create WebAPIs and dynamic Websites.

We will implement a minimalistic two-tier booking application, a Web API backend with local
DB, and a website frontend. The features you will need to implement in the Web API include:
• Allowing admin to log in. Admin username is “admin”, and the password is
“adminpass”. This is hard coded, so do not need any registration. General users do not
need to log in. [0.5 Mark]
• Allowing admin to add new centre names to its centre lists. [0.5 Mark]
• Allowing all users (including the admin) to retrieve the list of centres. [1 Mark]
• Allowing users to book a centre from a start date to an end date if it is not already
• booked (no overlapping. if user X books from 1/1/2022 to 3/1/2022, the user Y cannot
book the dame centre from 2/1/2022 to 8/1/2022 as the time periods overlaps). The
user has to provide their name if they can book the centre. [3 Marks]
• providing users with the next available start date for a given centre. [1 Marks]
• Allowing admin to view all the bookings (booking person name, start date and end
• date) for a given canter. [2 Marks]

The Website will need to host a single-page web app that allows users to:
• Log in for admin [ 0.5 Mark]
• View for admin to show all centres. [0.5 Mark]
• View for admin to add a new centre. [0.5 Mark]
• View for admin to select a centre and show all bookings. [0.5 Marks]
• View for non-admin users to show all centres. [0.5 Mark]
• View for non-admin users to search centres names (partial string match should
• produce a result) [2 Marks]
• View for non-admin to select a centre and show the next available start date. [0.5
Mark]
• View for non-admin to select a centre and book it from a start date to an end date (if
available). Make sure the start date should be greater and equal to the current date,
i.e., we don’t allow any booking on back dates. [2 Marks]

You are open to creating your own Database Schema. You must use the libraries and
methods taught this semester (MVC ASP.NET, Newtonsoft.JSON, JQuery AJAX, etc.) to do
this. Choices of design and functionality beyond the minimum requirements are up to you!
