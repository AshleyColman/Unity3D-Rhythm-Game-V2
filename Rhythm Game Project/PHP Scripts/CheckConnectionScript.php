<?php

// Database variables
$dbServername = "localhost";
$dbUsername = "root";
$dbPassword = "";
$dbName = "rhythmgamex";

// Connection strings
$error = "error";

// Connect to database
$con = mysqli_connect($dbServername, $dbUsername, $dbPassword, $dbName);

// Check connection to database
if (mysqli_connect_errno())
{
    echo $error;
    exit();
}
else
{
    exit();
}
?>