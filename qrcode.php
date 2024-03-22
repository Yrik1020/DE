<?php
/*
Plugin Name: QR Code Page Plugin
Description: This plugin generates a QR code containing the link to the current page.
*/

// Enqueue QRCode.js library
function enqueue_qrcode_library() {
    wp_enqueue_script('qrcode-js', 'https://cdn.jsdelivr.net/npm/qrcode@1.4.4/qrcode.min.js', array(), '1.4.4', true);
}
add_action('wp_enqueue_scripts', 'enqueue_qrcode_library');

// Generate QR code and return HTML
function generate_qr_code() {
    global $post;
    
    // Get current page URL
    $url = get_permalink($post->ID);
    
    // Generate QR code using QRCode.js
    $qr_code_script = "<script>
        var qr = qrcode(0, 'M');
        qr.addData('".$url."');
        qr.make();
        document.write(qr.createImgTag());
    </script>";
    
    return $qr_code_script;
}

// Register [qrpage] shortcode
function qr_page_shortcode() {
    return generate_qr_code();
}
add_shortcode('qrpage', 'qr_page_shortcode');
?>
