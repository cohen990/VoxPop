<?php

function mh_customize_register($wp_customize) {

	/***** Register Custom Controls *****/
	
	class MH_Customize_Textarea_Control extends WP_Customize_Control {
    	public $type = 'textarea';
    	public function render_content() { ?>
            <label>
                <span class="customize-textarea"><?php echo esc_html($this->label); ?></span>
                <textarea rows="5" style="width: 100%;" <?php $this->link(); ?>><?php echo esc_textarea($this->value()); ?></textarea>
            </label> <?php
	    }
	}

	/***** Add Sections *****/

	$wp_customize->add_section('mh_general', array('title' => __('General Options', 'mh'), 'priority' => 1));
	$wp_customize->add_section('mh_layout', array('title' => __('Layout Options', 'mh'), 'priority' => 2));
	$wp_customize->add_section('mh_content', array('title' => __('Posts/Pages Options', 'mh'), 'priority' => 5));
    $wp_customize->add_section('mh_css', array('title' => __('Custom CSS', 'mh'), 'priority' => 8));
    $wp_customize->add_section('mh_tracking', array('title' => __('Tracking Code', 'mh'), 'priority' => 9));
   
    /***** Add Settings *****/
    
    $wp_customize->add_setting('mh_options[full_bg]', array('default' => '', 'type' => 'option', 'sanitize_callback' => 'mh_sanitize_checkbox'));    
    $wp_customize->add_setting('mh_options[no_prettyphoto]', array('default' => '', 'type' => 'option', 'sanitize_callback' => 'mh_sanitize_checkbox'));    
    $wp_customize->add_setting('mh_options[excerpt_length]', array('default' => '125', 'type' => 'option', 'sanitize_callback' => 'mh_sanitize_integer'));
    $wp_customize->add_setting('mh_options[excerpt_more]', array('default' => '[...]', 'type' => 'option', 'sanitize_callback' => 'mh_sanitize_text'));
        
    $wp_customize->add_setting('mh_options[sb_position]', array('default' => 'right', 'type' => 'option', 'sanitize_callback' => 'mh_sanitize_select'));   
            
    $wp_customize->add_setting('mh_options[author_box]', array('default' => '', 'type' => 'option', 'sanitize_callback' => 'mh_sanitize_checkbox'));        
    $wp_customize->add_setting('mh_options[comments_pages]', array('default' => '', 'type' => 'option', 'sanitize_callback' => 'mh_sanitize_checkbox'));       
    $wp_customize->add_setting('mh_options[post_nav]', array('default' => '', 'type' => 'option', 'sanitize_callback' => 'mh_sanitize_checkbox'));    
    $wp_customize->add_setting('mh_options[related_posts]', array('default' => '', 'type' => 'option', 'sanitize_callback' => 'mh_sanitize_checkbox'));
            
    $wp_customize->add_setting('mh_options[custom_css]', array('default' => '', 'type' => 'option'));
    $wp_customize->add_setting('mh_options[tracking_code]', array('default' => '', 'type' => 'option'));
    
    /***** Add Controls *****/
        
    $wp_customize->add_control('full_bg', array('label' => __('Scale background image to full size', 'mh'), 'section' => 'mh_general', 'settings' => 'mh_options[full_bg]', 'priority' => 1, 'type' => 'checkbox'));        
    $wp_customize->add_control('no_prettyphoto', array('label' => __('Disable prettyPhoto lightbox', 'mh'), 'section' => 'mh_general', 'settings' => 'mh_options[no_prettyphoto]', 'priority' => 2, 'type' => 'checkbox'));
    $wp_customize->add_control('excerpt_length', array('label' => __('Custom excerpt length in characters', 'mh'), 'section' => 'mh_general', 'settings' => 'mh_options[excerpt_length]', 'priority' => 4, 'type' => 'text'));
    $wp_customize->add_control('excerpt_more', array('label' => __('Custom excerpt more text', 'mh'), 'section' => 'mh_general', 'settings' => 'mh_options[excerpt_more]', 'priority' => 5, 'type' => 'text'));    

    $wp_customize->add_control('sb_position', array('label' => __('Position of default sidebar', 'mh'), 'section' => 'mh_layout', 'settings' => 'mh_options[sb_position]', 'priority' => 7, 'type' => 'select', 'choices' => array('left' => __('Left', 'mh'), 'right' => __('Right', 'mh'))));  
    	    
    $wp_customize->add_control('author_box', array('label' => __('Disable author box on posts/archives', 'mh'), 'section' => 'mh_content', 'settings' => 'mh_options[author_box]', 'priority' => 7, 'type' => 'checkbox'));          
    $wp_customize->add_control('comments_pages', array('label' => __('Enable comments on pages', 'mh'), 'section' => 'mh_content', 'settings' => 'mh_options[comments_pages]', 'priority' => 9, 'type' => 'checkbox'));       
    $wp_customize->add_control('post_nav', array('label' => __('Enable post / attachment navigation', 'mh'), 'section' => 'mh_content', 'settings' => 'mh_options[post_nav]', 'priority' => 10, 'type' => 'checkbox'));   
    $wp_customize->add_control('related_posts', array('label' => __('Enable related articles on posts', 'mh'), 'section' => 'mh_content', 'settings' => 'mh_options[related_posts]', 'priority' => 11, 'type' => 'checkbox'));
               
    $wp_customize->add_control(new MH_Customize_Textarea_Control($wp_customize, 'custom_css', array('label' => __('Custom CSS', 'mh'), 'section' => 'mh_css', 'settings' => 'mh_options[custom_css]', 'priority' => 1)));
    $wp_customize->add_control(new MH_Customize_Textarea_Control($wp_customize, 'tracking_code', array('label' => __('Tracking Code (e.g. Google Analytics)', 'mh'), 'section' => 'mh_tracking', 'settings' => 'mh_options[tracking_code]', 'priority' => 1)));
}
add_action('customize_register', 'mh_customize_register');

/***** Data Sanitization *****/

function mh_sanitize_text($input) {
    return wp_kses_post(force_balance_tags($input));
}
function mh_sanitize_integer($input) {
    return strip_tags($input);
}
function mh_sanitize_checkbox($input) {
    if ($input == 1) {
        return 1;
    } else {
        return '';
    }
}
function mh_sanitize_select($input) {
    $valid = array(
        'left' => __('Left', 'mh'),
        'right' => __('Right', 'mh')
    ); 
    if (array_key_exists($input, $valid)) {
        return $input;
    } else {
        return '';
    }
}

/***** CSS Output *****/

function mh_custom_css() {
	$options = get_option('mh_options');    
	if ($options['custom_css']) {	
    	echo '<style type="text/css">' . "\n";
			echo $options['custom_css'] . "\n";
		echo '</style>' . "\n";
	}
}
add_action('wp_head', 'mh_custom_css');

?>