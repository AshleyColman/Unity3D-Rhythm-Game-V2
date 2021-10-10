<?php

$dbServername = "localhost";
$dbUsername = "root";
$dbPassword = "";
$dbName = "rhythmgamex";

$con = mysqli_connect($dbServername, $dbUsername, $dbPassword, $dbName);

$username = $_POST["username"];
$errorCode = "error";

// Check the connect happened
if (mysqli_connect_errno())
{
    // Error code #1 = connection failed
    echo $errorCode;
    exit();
}
else
{
    // Retrieve all play information from beatmap leaderboard
    $sql = "SELECT `username`, `date_joined`, `level`, `image_url`, `banner_image_url`, `ranked_play_time`, `ranked_score`, `ranked_combo`, 
    `ranked_objects_hit`, `ranked_perfect`, `ranked_great`, `ranked_okay`, `ranked_miss`, `ranked_grade_s+`, `ranked_grade_s`, 
    `ranked_grade_a+`, `ranked_grade_a`, `ranked_grade_b+`, `ranked_grade_b`, `ranked_grade_c+`, `ranked_grade_c`, `ranked_grade_d+`,
    `ranked_grade_d`, `ranked_grade_e+`, `ranked_grade_e`, `ranked_grade_f+`, `ranked_grade_f`,
    `ranked_clear_points`, `ranked_hidden_points`, `ranked_mine_points`, `ranked_low_ar_points`, `ranked_high_ar_points`,
    `ranked_full_combo_points`, `ranked_max_percentage_points`, `ranked_fails`, `ranked_two_key_score`, `ranked_four_key_score`, 
    `ranked_six_key_score`,`total_play_time`, `total_score`, `total_combo`, `total_objects_hit`, `total_perfect`, `total_great`, 
    `total_okay`, `total_miss`, `total_grade_s+`, `total_grade_s`, `total_grade_a+`, `total_grade_a`, `total_grade_b+`, 
    `total_grade_b`, `total_grade_c+`, `total_grade_c`, `total_grade_d+`, `total_grade_d`, `total_grade_e+`, `total_grade_e`, 
    `total_grade_f+`, `total_grade_f`, `total_clear_points`, `total_hidden_points`, `total_mine_points`, `total_low_ar_points`, 
    `total_high_ar_points`, `total_full_combo_points`, `total_max_percentage_points`, `total_fails`, `total_two_key_score`, 
    `total_four_key_score`, `total_six_key_score`
    FROM `players` WHERE username ='" . $username . "';";

    $sqlCheck = mysqli_query($con, $sql);
    
    if (mysqli_num_rows($sqlCheck) != 1)
    {
        echo $errorCode;
        exit();
    }
    
    // Get information from table
    $infoarray = mysqli_fetch_assoc($sqlCheck);

    // Assign to array
    $returnarray[0] = $infoarray["username"];
    $returnarray[1] = $infoarray["date_joined"];
    $returnarray[2] = $infoarray["level"];
    $returnarray[3] = $infoarray["image_url"];
    $returnarray[4] = $infoarray["banner_image_url"];

    $returnarray[5] = $infoarray["ranked_play_time"];
    $returnarray[6] = $infoarray["ranked_score"];
    $returnarray[7] = $infoarray["ranked_combo"];
    $returnarray[8] = $infoarray["ranked_objects_hit"];
    $returnarray[9] = $infoarray["ranked_perfect"];
    $returnarray[10] = $infoarray["ranked_great"];
    $returnarray[11] = $infoarray["ranked_okay"];
    $returnarray[12] = $infoarray["ranked_miss"];
    $returnarray[13] = $infoarray["ranked_grade_s+"];
    $returnarray[14] = $infoarray["ranked_grade_s"];
    $returnarray[15] = $infoarray["ranked_grade_a+"];
    $returnarray[16] = $infoarray["ranked_grade_a"];
    $returnarray[17] = $infoarray["ranked_grade_b+"];
    $returnarray[18] = $infoarray["ranked_grade_b"];
    $returnarray[19] = $infoarray["ranked_grade_c+"];
    $returnarray[20] = $infoarray["ranked_grade_c"];
    $returnarray[21] = $infoarray["ranked_grade_d+"];
    $returnarray[22] = $infoarray["ranked_grade_d"];
    $returnarray[23] = $infoarray["ranked_grade_e+"];
    $returnarray[24] = $infoarray["ranked_grade_e"];
    $returnarray[25] = $infoarray["ranked_grade_f+"];
    $returnarray[26] = $infoarray["ranked_grade_f"];
    $returnarray[27] = $infoarray["ranked_clear_points"];
    $returnarray[28] = $infoarray["ranked_hidden_points"];
    $returnarray[29] = $infoarray["ranked_mine_points"];
    $returnarray[30] = $infoarray["ranked_low_ar_points"];
    $returnarray[31] = $infoarray["ranked_high_ar_points"];
    $returnarray[32] = $infoarray["ranked_full_combo_points"];
    $returnarray[33] = $infoarray["ranked_max_percentage_points"];
    $returnarray[34] = $infoarray["ranked_fails"];
    $returnarray[35] = $infoarray["ranked_two_key_score"];
    $returnarray[36] = $infoarray["ranked_four_key_score"];
    $returnarray[37] = $infoarray["ranked_six_key_score"];

    $returnarray[38] = $infoarray["total_play_time"];
    $returnarray[39] = $infoarray["total_score"];
    $returnarray[40] = $infoarray["total_combo"];
    $returnarray[41] = $infoarray["total_objects_hit"];
    $returnarray[42] = $infoarray["total_perfect"];
    $returnarray[43] = $infoarray["total_great"];
    $returnarray[44] = $infoarray["total_okay"];
    $returnarray[45] = $infoarray["total_miss"];
    $returnarray[46] = $infoarray["total_grade_s+"];
    $returnarray[47] = $infoarray["total_grade_s"];
    $returnarray[48] = $infoarray["total_grade_a+"];
    $returnarray[49] = $infoarray["total_grade_a"];
    $returnarray[50] = $infoarray["total_grade_b+"];
    $returnarray[51] = $infoarray["total_grade_b"];
    $returnarray[52] = $infoarray["total_grade_c+"];
    $returnarray[53] = $infoarray["total_grade_c"];
    $returnarray[54] = $infoarray["total_grade_d+"];
    $returnarray[55] = $infoarray["total_grade_d"];
    $returnarray[56] = $infoarray["total_grade_e+"];
    $returnarray[57] = $infoarray["total_grade_e"];
    $returnarray[58] = $infoarray["total_grade_f+"];
    $returnarray[59] = $infoarray["total_grade_f"];
    $returnarray[60] = $infoarray["total_clear_points"];
    $returnarray[61] = $infoarray["total_hidden_points"];
    $returnarray[62] = $infoarray["total_mine_points"];
    $returnarray[63] = $infoarray["total_low_ar_points"];
    $returnarray[64] = $infoarray["total_high_ar_points"];
    $returnarray[65] = $infoarray["total_full_combo_points"];
    $returnarray[66] = $infoarray["total_max_percentage_points"];
    $returnarray[67] = $infoarray["total_fails"];
    $returnarray[68] = $infoarray["total_two_key_score"];
    $returnarray[69] = $infoarray["total_four_key_score"];
    $returnarray[70] = $infoarray["total_six_key_score"];

    // Split array and send back to C# script
    die (implode("->", $returnarray));
}
?>