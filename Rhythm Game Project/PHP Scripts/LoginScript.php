<?php

$dbServername = "localhost";
$dbUsername = "root";
$dbPassword = "";
$dbName = "rhythmgamex";

$connectError = "connectError";
$usernameCheckQueryError = "usernameCheckQueryError";
$usernameDoesNotExistError = "usernameDoesNotExistError";
$wrongPasswordError = "wrongPasswordError";
$success = "success";

$con = mysqli_connect($dbServername, $dbUsername, $dbPassword, $dbName);

if (mysqli_connect_errno())
{
    echo $connectError;
    exit();
}

$username = $_POST["username"];
$password = $_POST["password"];

// Check if username exists in the database.
$usernameCheckQuery = "SELECT username, salt, hash FROM players WHERE username ='" . $username . "';";

$usernameCheck = mysqli_query($con, $usernameCheckQuery) or die($usernameCheckQueryError);

if (mysqli_num_rows($usernameCheck) != 1)
{
    echo $usernameDoesNotExistError;
    exit();
} 

// Get login info.
$existingInfo = mysqli_fetch_assoc($usernameCheck);
$salt = $existingInfo["salt"];
$hash = $existingInfo["hash"];

$loginHash = crypt($password, $salt);

if ($hash != $loginHash)
{
    echo $wrongPasswordError;
    exit();
}
else
{
    echo $success;
}
?>