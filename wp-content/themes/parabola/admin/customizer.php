<?php
/**
 * Contains methods for customizing the theme customization screen.
 * @since Parabola 1.4.1
 */

function parabola_customizer_setup($wp_customize) {
	class Parabola_Extended_Settings extends WP_Customize_Control {
		public $type = 'parabola_extended_link';
		public function render_content() {
			_e(
				sprintf('To configure the remaining 200+ theme options, access the dedicated %s page.',
						'<b><a href="themes.php?page=parabola-page">'.
							__('Parabola Settings','parabola').
						'</a></b>'),
						'parabola'
			);
		}
	}  // class
} // parabola_customizer_setup()
 
class Parabola_Customize {

   public static function register ( $wp_customize ) {
   
      $wp_customize->add_section( 'parabola_settings', 
         array(
            'title' => __( 'Parabola Advanced Settings', 'parabola' ), //Visible title of section
            'priority' => 999, //Determines what order this appears in
            'capability' => 'edit_theme_options', //Capability needed to tweak
            'description' => __('', 'parabola'), //Descriptive tooltip
         ) 
      );
        
      $wp_customize->add_setting( 'parabola_extended_link', 
         array(
            'default' => '#', //Default setting/value to save
            'type' => 'theme_mod', //Is this an 'option' or a 'theme_mod'?
            'capability' => 'edit_theme_options', //Optional. Special permissions for accessing this setting.
            'transport' => 'postMessage', //What triggers a refresh of the setting? 'refresh' or 'postMessage' (instant)?
         ) 
      ); 	  

	  $wp_customize->add_control( new Parabola_Extended_Settings( 
		$wp_customize, 
		'parabola_extended_link', 
		array(
			'label'   => '',
			'section' => 'parabola_settings',
			'settings'   => 'parabola_extended_link',
		)
	  ) );
   
   }
 
}

// Setup the Theme Customizer settings and controls...
add_action( 'customize_register', 'parabola_customizer_setup' );
add_action( 'customize_register' , array( 'Parabola_Customize' , 'register' ) );

?>