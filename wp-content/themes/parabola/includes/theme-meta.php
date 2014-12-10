<?php /*
 * meta related functions
 *
 * @package parabola
 * @subpackage Functions
 */

/**
 * Filter for page meta title.
 */
function parabola_filter_wp_title( $title ) {
    // Get the Site Name
    $site_name = get_bloginfo( 'name' );
    // Prepend name
    $filtered_title = (((strlen($site_name)>0)&&(strlen($title)>0))?$title.' - '.$site_name:$title.$site_name);
	// Get the Site Description
 	$site_description = get_bloginfo( 'description' );
    // If site front page, append description
    if ( (is_home() || is_front_page()) && $site_description ) {
        // Append Site Description to title
        $filtered_title = ((strlen($site_name)>0)&&(strlen($site_description)>0))?$site_name. " | ".$site_description:$site_name.$site_description;
    }
	// Add pagination if that's the case
	global $page, $paged;
	if ( $paged >= 2 || $page >= 2 )
	$filtered_title .=	 ' | ' . sprintf( __( 'Page %s', 'parabola' ), max( $paged, $page ) );

    // Return the modified title
    return $filtered_title;
}

function parabola_filter_wp_title_rss($title) {
return ' ';
}
add_filter( 'wp_title', 'parabola_filter_wp_title' );
add_filter('wp_title_rss','parabola_filter_wp_title_rss');

/**
 * Meta Title
 */
function parabola_meta_title() {
global $parabolas;
echo "<title>".wp_title( '', false, 'right' )."</title>";
if ($parabolas['parabola_iecompat']): echo PHP_EOL.'<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />'; endif;
}

function parabola_mobile_meta() {
global $parabolas;
if ($parabolas['parabola_zoom']==1) 
	echo '<meta name="viewport" content="width=device-width, user-scalable=yes, initial-scale=1.0, minimum-scale=1.0, maximum-scale=3.0">';
else echo '<meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0">';
echo PHP_EOL;
}

add_action ('cryout_meta_hook','parabola_meta_title',0);
add_action ('cryout_meta_hook','parabola_mobile_meta');

// Parabola favicon
function parabola_fav_icon() {
global $parabolas;
foreach ($parabolas as $key => $value) {
${"$key"} = $value ;}
	 echo '<link rel="shortcut icon" href="'.esc_url($parabolas['parabola_favicon']).'" />';
	 echo '<link rel="apple-touch-icon" href="'.esc_url($parabolas['parabola_favicon']).'" />';
	}

if ($parabolas['parabola_favicon']) add_action ('cryout_header_hook','parabola_fav_icon');


?>