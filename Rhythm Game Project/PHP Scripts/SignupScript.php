<?php

$dbServername = "localhost";
$dbUsername = "root";
$dbPassword = "";
$dbName = "rhythmgamex";

$connectError = "connectError";
$usernameExistError = "userExistError";
$usernameCheckQueryError = "usernameCheckQueryError";
$usernameInsertQueryError = "usernameInsertQueryError";
$success = "success";

$con = mysqli_connect($dbServername, $dbUsername, $dbPassword, $dbName);

if (mysqli_connect_errno())
{
    echo $connectError;
    exit();
}

$username = $_POST["username"];
$password = $_POST["password"];

// Check if username exists in the database already.
$usernamecheckquery = "SELECT username FROM players WHERE username ='" . $username . "';";

$usernamecheck = mysqli_query($con, $usernamecheckquery) or die($usernameCheckQueryError);

if (mysqli_num_rows($usernamecheck) > 0)
{
    // User already exists.
    echo $usernameExistError;
    exit();
}

// Add user to the table.
$salt = "\$5\$rounds=5000\$" . "rhythmgamex" . $username . "\$";
$hash = crypt($password, $salt);
$insertuserquery = "INSERT INTO players (username, hash, salt) VALUES ('" . $username . "', '" . $hash . "', '" . $salt . "');";

mysqli_query($con, $insertuserquery) or die($usernameInsertQueryError);

echo ($success);
?>