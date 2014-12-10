<?php

/***** Custom Dashboard Widget *****/ 

function mh_info_widget() {
	echo '<div class="admin-theme-thumb"><img src="' . get_template_directory_uri() . '/images/logo.png" /></div>' . "\n";
	echo '<p>Thank you very much for downloading <strong>MH Magazine <em>lite</em> - WordPress Theme</strong>! For support, general questions and more information please have a look at the: </p>' . "\n";
	echo '<ul>' . "\n";
		echo '<li><a href="http://www.mhthemes.com/documentation-mh-magazine-lite/" target="_blank"><strong>Quick Theme Documentation</strong></a></li>' . "\n";
		echo '<li><a href="http://www.mhthemes.com/faq/" target="_blank"><strong>Frequently Asked Questions</strong></a></li>' . "\n";
		echo '<li><a href="http://wordpress.org/support/theme/mh-magazine-lite" target="_blank"><strong>WordPress Support Forum</strong></a></li>' . "\n"; 
	echo '</ul>' . "\n";
	echo '<p><strong>Upgrade:</strong> If you want your magazine to be fully responsive with a lot more features and options, you can <a href="http://www.mhthemes.com/demo/" target="_blank"><strong>purchase the premium version of MH Magazine</strong></a>. To stay up-to-date you can follow us on <a href="https://www.facebook.com/MHthemes" target="_blank"><strong>Facebook</strong></a>, <a href="https://twitter.com/MHthemes" target="_blank"><strong>Twitter</strong></a> and/or <a href="https://plus.google.com/u/0/116858061109988982542/" target="_blank"><strong>Google+</strong></a>.</p></p>' . "\n";
}

function mh_dashboard_widgets() {
	global $wp_meta_boxes;
	add_meta_box('mh_info_widget', 'Theme Support: Get started!', 'mh_info_widget', 'dashboard', 'normal', 'high');
}
add_action('wp_dashboard_setup', 'mh_dashboard_widgets');

/***** Custom Meta Boxes *****/

if (!function_exists('mh_add_meta_boxes')) {
	function mh_add_meta_boxes() {
		global $options;
		add_meta_box('mh_post_details', __('Post Options', 'mh'), 'mh_post_options', 'post', 'normal', 'high');
	}
}
add_action('add_meta_boxes', 'mh_add_meta_boxes');

if (!function_exists('mh_post_options')) {
	function mh_post_options() {
		global $post;
		wp_nonce_field('mh_meta_box_nonce', 'meta_box_nonce'); 
		echo '<p>';
		echo '<label for="mh-subheading">' . __("Subheading (will be displayed below post title)", 'mh') . '</label>';
		echo '<br />';
		echo '<input class="widefat" type="text" name="mh-subheading" id="mh-subheading" placeholder="Enter subheading" value="' . esc_attr(get_post_meta($post->ID, 'mh-subheading', true)) . '" size="30" />';
		echo '</p>';
	}
}

if (!function_exists('mh_save_meta_boxes')) {
	function mh_save_meta_boxes($post_id, $post) {
		if (!isset($_POST['meta_box_nonce']) || !wp_verify_nonce($_POST['meta_box_nonce'], 'mh_meta_box_nonce')) {
			return $post->ID;
		}
		if (defined('DOING_AUTOSAVE') && DOING_AUTOSAVE) {
        	return $post->ID;
		}
		if ('page' == $_POST['post_type']) {
			if (!current_user_can('edit_page', $post_id)) {
				return $post->ID;
			}
		} 
		elseif (!current_user_can('edit_post', $post_id)) {
			return $post->ID;
		}
		if ('post' == $_POST['post_type']) {
			$meta_data['mh-subheading'] = esc_attr($_POST['mh-subheading']);
		}
		foreach ($meta_data as $key => $value) {
			if ($post->post_type == 'revision') return;
			$value = implode(',', (array)$value);
			if (get_post_meta($post->ID, $key, FALSE)) {
				update_post_meta($post->ID, $key, $value);
			} else {
				add_post_meta($post->ID, $key, $value);
			}
			if (!$value) delete_post_meta($post->ID, $key);
		}
	}
}
add_action('save_post', 'mh_save_meta_boxes', 10, 2 );

?>