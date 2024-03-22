<?php
/*
Plugin Name: Random Line Plugin
Description: This plugin displays a random line from a specified post on any page of the website.
*/

// Add plugin settings page
function random_line_plugin_menu() {
    add_options_page('Random Line Settings', 'Random Line Settings', 'manage_options', 'random-line-settings', 'random_line_settings_page');
}
add_action('admin_menu', 'random_line_plugin_menu');

// Settings page content
function random_line_settings_page() {
    ?>
    <div class="wrap">
        <h2>Random Line Settings</h2>
        <form method="post" action="options.php">
            <?php
            settings_fields('random_line_settings');
            do_settings_sections('random_line_settings');
            submit_button();
            ?>
        </form>
    </div>
    <?php
}

// Register and initialize plugin settings
function random_line_plugin_settings() {
    register_setting('random_line_settings', 'random_line_post_id');
    add_settings_section('random_line_section', 'Post Settings', 'random_line_section_callback', 'random_line_settings');
    add_settings_field('random_line_post_id', 'Post ID', 'random_line_post_id_callback', 'random_line_settings', 'random_line_section');
}
add_action('admin_init', 'random_line_plugin_settings');

// Section callback
function random_line_section_callback() {
    echo '<p>Specify the post from which to display random lines.</p>';
}

// Post ID field callback
function random_line_post_id_callback() {
    $post_id = get_option('random_line_post_id');
    echo "<input type='text' id='random_line_post_id' name='random_line_post_id' value='$post_id' />";
}

// Display random line
function display_random_line() {
    $post_id = get_option('random_line_post_id');
    if ($post_id) {
        $post = get_post($post_id);
        if ($post) {
            $lines = explode("\n", $post->post_content);
            $random_line = $lines[array_rand($lines)];
            echo "<div id='random-line'>$random_line</div>";
        }
    }
}
add_action('wp_footer', 'display_random_line');
?>
