<?php

$dbServername = "localhost";
$dbUsername = "root";
$dbPassword = "";
$dbName = "rhythmgamex";

$con = mysqli_connect($dbServername, $dbUsername, $dbPassword, $dbName);

$databaseTable = $_POST["databaseTable"];
$error = "error";
$success = "success";

// Check the connect happened
if (mysqli_connect_errno())
{
    echo $error;
    exit();
}
else
{
    $sql = "CREATE TABLE `rhythmgamex`.`$databaseTable` 
    ( `id` INT NOT NULL AUTO_INCREMENT ,
    `username` VARCHAR(15) NOT NULL,
    `score` INT NOT NULL ,
    `approach_rate` INT NOT NULL ,
    `object_size` INT NOT NULL ,
    `health_point` INT NOT NULL ,
    `speed` INT NOT NULL ,
    `experience` INT NOT NULL ,
    `combo` INT NOT NULL ,
    `hit` INT NOT NULL ,
    `miss` INT NOT NULL ,
    `activation` INT NOT NULL ,
    `accuracy` FLOAT NOT NULL ,
    `date` DATETIME NOT NULL ,
    PRIMARY KEY (`id`)) ENGINE = InnoDB;";

    if (mysqli_query($con, $sql)) {
    echo $success; 
   } else {
    echo $error;
   }
    exit();
}
?>