<?php

/***** Fetch Options *****/

$options = get_option('mh_options');

/***** Custom Hooks *****/

function mh_html_class() {
    do_action('mh_html_class');
}
function mh_content_class() {
    do_action('mh_content_class');
}
function mh_sb_class() {
    do_action('mh_sb_class');
}
function mh_before_page_content() {
    do_action('mh_before_page_content');
}
function mh_before_post_content() {
    do_action('mh_before_post_content');
}
function mh_after_post_content() {
    do_action('mh_after_post_content');
}
function mh_post_header() {
    do_action('mh_post_header');
}
function mh_loop_content() {
    do_action('mh_loop_content');
}

/***** Enable Shortcodes inside Widgets	*****/

add_filter('widget_text', 'do_shortcode');

/***** Theme Setup *****/

function mh_themes_setup() {

	global $content_width;

	if (!isset($content_width)) {
		$content_width = 620;
	}

	$header = array(
		'default-image' => '',
		'width' => 300,
		'height' => 100,
		'flex-width' => true,
		'flex-height' => true,
		'header-text' => false
	);
	add_theme_support('custom-header', $header);

	load_theme_textdomain('mh', get_template_directory() . '/languages');
	add_theme_support('automatic-feed-links');
	add_theme_support('custom-background');
	add_theme_support('post-thumbnails');
	add_image_size('content', 620, 264, true);
	add_image_size('loop', 174, 131, true);
	add_image_size('cp_small', 70, 53, true);
	add_editor_style();
	register_nav_menu('main_nav', __('Main Navigation', 'mh'));
}
add_action('after_setup_theme', 'mh_themes_setup');

/***** Load CSS & JavaScript *****/

if (!function_exists('mh_scripts')) {
	function mh_scripts() {
		wp_enqueue_style('mh-google-fonts', '//fonts.googleapis.com/css?family=Open+Sans:400,400italic,700,600', array(), null);
		wp_enqueue_style('mh-style', get_stylesheet_uri(), false, '1.6.2');
		wp_enqueue_script('scripts', get_template_directory_uri() . '/js/scripts.js', array('jquery'));
		if (!is_admin()) {
			if (is_singular() && comments_open() && (get_option('thread_comments') == 1))
				wp_enqueue_script('comment-reply');
		}
	}
}
add_action('wp_enqueue_scripts', 'mh_scripts');

if (!function_exists('mh_admin_scripts')) {
	function mh_admin_scripts($hook) {
		wp_enqueue_style('mh-admin', get_template_directory_uri() . '/admin/admin.css');
	}
}
add_action('admin_enqueue_scripts', 'mh_admin_scripts');

/***** Register Widget Areas / Sidebars	*****/

if (!function_exists('mh_widgets_init')) {
	function mh_widgets_init() {
		global $options;
		register_sidebar(array('name' => __('Sidebar', 'mh'), 'id' => 'sidebar', 'description' => __('Widget area (sidebar left/right) on single posts, pages and archives', 'mh'), 'before_widget' => '<div class="sb-widget">', 'after_widget' => '</div>', 'before_title' => '<h4 class="widget-title">', 'after_title' => '</h4>'));
		register_sidebar(array('name' => __('Home 1', 'mh'), 'id' => 'home-1', 'description' => __('Widget area on homepage', 'mh'), 'before_widget' => '<div class="sb-widget home-1 home-wide">', 'after_widget' => '</div>', 'before_title' => '<h4 class="widget-title">', 'after_title' => '</h4>'));
		register_sidebar(array('name' => __('Home 2', 'mh'), 'id' => 'home-2', 'description' => __('Widget area on homepage', 'mh'), 'before_widget' => '<div class="sb-widget home-2">', 'after_widget' => '</div>', 'before_title' => '<h4 class="widget-title">', 'after_title' => '</h4>'));
		register_sidebar(array('name' => __('Home 3', 'mh'), 'id' => 'home-3', 'description' => __('Widget area on homepage', 'mh'), 'before_widget' => '<div class="sb-widget home-3">', 'after_widget' => '</div>', 'before_title' => '<h4 class="widget-title">', 'after_title' => '</h4>'));
		register_sidebar(array('name' => __('Home 4', 'mh'), 'id' => 'home-4', 'description' => __('Widget area on homepage', 'mh'), 'before_widget' => '<div class="sb-widget home-4 home-wide">', 'after_widget' => '</div>', 'before_title' => '<h4 class="widget-title">', 'after_title' => '</h4>'));
		register_sidebar(array('name' => __('Home 5', 'mh'), 'id' => 'home-5', 'description' => __('Widget area on homepage', 'mh'), 'before_widget' => '<div class="sb-widget home-5">', 'after_widget' => '</div>', 'before_title' => '<h4 class="widget-title">', 'after_title' => '</h4>'));
		register_sidebar(array('name' => __('Posts 1', 'mh'), 'id' => 'posts-1', 'description' => __('Widget area above single post content', 'mh'), 'before_widget' => '<div class="sb-widget posts-1">', 'after_widget' => '</div>', 'before_title' => '<h4 class="widget-title">', 'after_title' => '</h4>'));
		register_sidebar(array('name' => __('Posts 2', 'mh'), 'id' => 'posts-2', 'description' => __('Widget area below single post content', 'mh'), 'before_widget' => '<div class="sb-widget posts-2">', 'after_widget' => '</div>', 'before_title' => '<h4 class="widget-title">', 'after_title' => '</h4>'));
		register_sidebar(array('name' => __('Footer 1', 'mh'), 'id' => 'footer-1', 'description' => __('Widget area in footer', 'mh'), 'before_widget' => '<div class="footer-widget footer-1">', 'after_widget' => '</div>', 'before_title' => '<h6 class="footer-widget-title">', 'after_title' => '</h6>'));
		register_sidebar(array('name' => __('Footer 2', 'mh'), 'id' => 'footer-2', 'description' => __('Widget area in footer', 'mh'), 'before_widget' => '<div class="footer-widget footer-2">', 'after_widget' => '</div>', 'before_title' => '<h6 class="footer-widget-title">', 'after_title' => '</h6>'));
		register_sidebar(array('name' => __('Footer 3', 'mh'), 'id' => 'footer-3', 'description' => __('Widget area in footer', 'mh'), 'before_widget' => '<div class="footer-widget footer-3">', 'after_widget' => '</div>', 'before_title' => '<h6 class="footer-widget-title">', 'after_title' => '</h6>'));
		register_sidebar(array('name' => __('Footer 4', 'mh'), 'id' => 'footer-4', 'description' => __('Widget area in footer', 'mh'), 'before_widget' => '<div class="footer-widget footer-4">', 'after_widget' => '</div>', 'before_title' => '<h6 class="footer-widget-title">', 'after_title' => '</h6>'));
	}
}
add_action('widgets_init', 'mh_widgets_init');

/***** Add CSS classes to HTML tag *****/

if (!function_exists('mh_html')) {
	function mh_html() {
		global $options;
		isset($options['full_bg']) && $options['full_bg'] == 1 ? $fullbg = ' fullbg' : $fullbg = '';
		echo $fullbg;
	}
}
add_action('mh_html_class', 'mh_html');

/***** Add CSS classes to content container *****/

if (!function_exists('mh_content_css')) {
	function mh_content_css() {
		global $options;
		if (isset($options['sb_position']) && $options['sb_position'] == 'left') {
			$float = 'right';
		} else {
			$float = 'left';
		}
		echo $float;
	}
}
add_action('mh_content_class', 'mh_content_css');

/***** Add CSS classes to sidebar container *****/

if (!function_exists('mh_sb_css')) {
	function mh_sb_css($sb_pos = '') {
		global $options;
		if (isset($options['sb_position']) && $options['sb_position'] == 'left') {
			$sb_pos = 'sb-left';
		} else {
			$sb_pos = 'sb-right';
		}
		echo $sb_pos;
	}
}
add_action('mh_sb_class', 'mh_sb_css');

/***** Logo / Header Image Fallback *****/

if (!function_exists('mh_logo')) {
	function mh_logo() {
		$header_img = get_header_image();
		echo '<div class="logo-wrap" role="banner">' . "\n";
		if ($header_img) {
			echo '<a href="' . esc_url(home_url('/')) . '" title="' . get_bloginfo('name') . '" rel="home"><img src="' . $header_img . '" height="' . get_custom_header()->height . '" width="' . get_custom_header()->width . '" alt="' . get_bloginfo('name') . '" /></a>' . "\n";
		} else {
			echo '<div class="logo">' . "\n";
			echo '<a href="' . esc_url(home_url('/')) . '" title="' . get_bloginfo('name') . '" rel="home">' . "\n";
			echo '<h1 class="logo-name">' . get_bloginfo('name') . '</h1>' . "\n";
			echo '<h2 class="logo-desc">' . get_bloginfo('description') . '</h2>' . "\n";
			echo '</a>' . "\n";
			echo '</div>' . "\n";
		}
		echo '</div>' . "\n";
	}
}

/***** wp_title Output *****/

if (!function_exists('mh_wp_title')) {
	function mh_wp_title($title, $sep) {
		global $paged, $page, $post;
		if (is_feed())
			return $title;
		$title .= get_bloginfo('name');
		$site_description = get_bloginfo('description', 'display');
		if ($site_description && (is_home() || is_front_page()))
			$title = "$title $sep $site_description";
		if ($paged >= 2 || $page >= 2)
			$title = "$title $sep " . sprintf(__('Page %s', 'mh'), max($paged, $page));
		return $title;
	}
}
add_filter('wp_title', 'mh_wp_title', 10, 2);

/***** Page Title Output *****/

if (!function_exists('mh_page_title')) {
	function mh_page_title() {
		if (!is_front_page()) {
			echo '<div class="page-title-top"></div>' . "\n";
			echo '<h1 class="page-title">';
			if (is_archive()) {
				if (is_category() || is_tax()) {
					single_cat_title();
				} elseif (is_tag()) {
					single_tag_title();
				} elseif (is_author()) {
					global $author;
					$user_info = get_userdata($author);
					printf(_x('Articles by %s', 'post author', 'mh'), esc_attr($user_info->display_name));
				} elseif (is_day()) {
					echo get_the_date();
				} elseif (is_month()) {
					echo get_the_date('F Y');
				} elseif (is_year()) {
					echo get_the_date('Y');
				} else {
					_e('Archives', 'mh');
				}
			} else {
				if (is_home()) {
					echo get_the_title(get_option('page_for_posts', true));
				} elseif (is_404()) {
					_e('Page not found (404)', 'mh');
				} elseif (is_search()) {
					printf(__('Search Results for %s', 'mh'), esc_attr(get_search_query()));
				} else {
					the_title();
				}
			}
			echo '</h1>' . "\n";
		}
	}
}
add_action('mh_before_page_content', 'mh_page_title');

/***** Subheading on Posts *****/

if (!function_exists('mh_subheading')) {
	function mh_subheading() {
		global $post;
		if (get_post_meta($post->ID, "mh-subheading", true)) {
			echo '<div class="subheading-top"></div>' . "\n";
			echo '<h2 class="subheading">' . esc_attr(get_post_meta($post->ID, "mh-subheading", true)) . '</h2>' . "\n";
		}
	}
}
add_action('mh_post_header', 'mh_subheading');

/***** Featured Image on Posts *****/

if (!function_exists('mh_featured_image')) {
	function mh_featured_image() {
		global $post, $options;
		if (has_post_thumbnail() && !is_attachment()) {
			$thumbnail = wp_get_attachment_image_src(get_post_thumbnail_id(), 'content');
			$full = wp_get_attachment_image_src(get_post_thumbnail_id(), 'full');
			$caption_text = get_post(get_post_thumbnail_id())->post_excerpt;
			if (isset($options['no_prettyphoto']) ? !$options['no_prettyphoto'] : true) {
				$attachment_url = '<a href="' . $full[0] . '" rel="prettyPhoto">';
			} else {
				$attachment_page = get_attachment_link(get_post_thumbnail_id());
				$attachment_url = '<a href="' . $attachment_page . '">';
			}
			echo "\n" . '<div class="post-thumbnail">' . "\n";
				echo $attachment_url . '<img src="' . $thumbnail[0] . '" alt="' . get_post_meta(get_post_thumbnail_id(), '_wp_attachment_image_alt', true) . '" title="' . get_post(get_post_thumbnail_id())->post_title . '" /></a>' . "\n";
				if ($caption_text) {
					echo '<span class="wp-caption-text">' . $caption_text . '</span>' . "\n";
				}
			echo '</div>' . "\n";
		}
	}
}

/***** Author box *****/

if (!function_exists('mh_author_box')) {
	function mh_author_box($author_ID = '') {
		global $options;
		if (isset($options['author_box'])) {
			if (!$options['author_box'] && !is_attachment() && get_the_author_meta('description', $author_ID)) {
				$author_ID = get_the_author_meta('ID');
				$ab_output = true;
			} else {
				$ab_output = false;
			}
		} elseif (!is_attachment() && get_the_author_meta('description', $author_ID)) {
			$author_ID = get_the_author_meta('ID');
			$ab_output = true;
		} else {
			$ab_output = false;
		}
		if ($ab_output) {
			$name = get_the_author_meta('display_name', $author_ID);
			echo '<section class="author-box">' . "\n";
				echo '<div class="author-box-wrap clearfix">' . "\n";
					echo '<div class="author-box-avatar">' . get_avatar($author_ID, 113) . '</div>' . "\n";
					echo '<h5 class="author-box-name">' . __('About ', 'mh') . esc_attr($name) . '</h5>' . "\n";
					echo '<div class="author-box-desc">' . get_the_author_meta('description', $author_ID) . '</div>' . "\n";
				echo '</div>' . "\n";
			echo '</section>' . "\n";
		}
	}
}
add_action('mh_after_post_content', 'mh_author_box');

/***** Post / Image Navigation *****/

if (!function_exists('mh_postnav')) {
	function mh_postnav() {
		global $post, $options;
		if (isset($options['post_nav']) && $options['post_nav']) {
			$parent_post = get_post($post->post_parent);
			$attachment = is_attachment();
			$previous = ($attachment) ? $parent_post : get_adjacent_post(false, '', true);
			$next = get_adjacent_post(false, '', false);

			if (!$next && !$previous)
			return;

			if ($attachment) {
				$attachments = get_children(array('post_type' => 'attachment', 'post_mime_type' => 'image', 'post_parent' => $parent_post->ID));
				$count = count($attachments);
			}
			if ($attachment && $count == 1) {
				$permalink = get_permalink($parent_post);
				echo '<nav class="section-title clearfix" role="navigation">' . "\n";
				echo '<div class="post-nav left">' . "\n";
				echo '<a href="' . $permalink . '">' . __('&larr; Back to article', 'mh') . '</a>';
				echo '</div>' . "\n";
				echo '</nav>' . "\n";
			} elseif (!$attachment || $attachment && $count > 1) {
				echo '<nav class="section-title clearfix" role="navigation">' . "\n";
				echo '<div class="post-nav left">' . "\n";
				if ($attachment) {
					previous_image_link('%link', __('&larr; Previous image', 'mh'));
				} else {
					previous_post_link('%link', __('&larr; Previous article', 'mh'));
				}
				echo '</div>' . "\n";
				echo '<div class="post-nav right">' . "\n";
				if ($attachment) {
					next_image_link('%link', __('Next image &rarr;', 'mh'));
				} else {
					next_post_link('%link', __('Next article &rarr;', 'mh'));
				}
				echo '</div>' . "\n";
				echo '</nav>' . "\n";
			}
		}
	}
}
add_action('mh_after_post_content', 'mh_postnav');

/***** Related Posts *****/

if (!function_exists('mh_related')) {
	function mh_related() {
		global $post, $options;
		if (isset($options['related_posts']) ? $options['related_posts'] : false) {
			$tags = wp_get_post_tags($post->ID);
			if ($tags) {
				$tag_ids = array();
				foreach($tags as $tag) $tag_ids[] = $tag->term_id;
				$args = array('tag__in' => $tag_ids, 'post__not_in' => array($post->ID), 'posts_per_page' => 7, 'ignore_sticky_posts' => 1, 'orderby' => 'rand');
				$related = new wp_query($args);
				if ($related->have_posts()) {
					echo '<section class="related-posts">' . "\n";
					echo '<h3 class="section-title">' . __('Related Articles', 'mh') . '</h3>' . "\n";
					echo '<div class="related-wrap clearfix">' . "\n";
					while ($related->have_posts()) : $related->the_post();
						$permalink = get_permalink($post->ID);
						echo '<div class="related-thumb">' . "\n";
						echo '<a href="' . $permalink . '" title="' . get_the_title() . '">' . "\n";
						if (has_post_thumbnail()) {
							the_post_thumbnail('cp_small');
						} else {
							echo '<img src="' . get_template_directory_uri() . '/images/noimage_70x53.png' . '" alt="No Picture" />' . "\n";
						}
						echo '</a>' . "\n";
						echo '</div>' . "\n";
					endwhile;
					echo '</div>' . "\n";
					echo '</section>' . "\n";
					wp_reset_postdata();
				}
			}
		}
	}
}
add_action('mh_after_post_content', 'mh_related');

/***** Loop Output *****/

if (!function_exists('mh_loop')) {
	function mh_loop() {
		if (have_posts()) {
			while (have_posts()) : the_post();
				get_template_part('loop', get_post_format());
			endwhile;
			mh_pagination();
		} else {
			get_template_part('content', 'none');
		}
	}
}
add_action('mh_loop_content', 'mh_loop');

/***** Custom Excerpts *****/

if (!function_exists('mh_trim_excerpt')) {
	function mh_trim_excerpt($text = '') {
		$raw_excerpt = $text;
		if ('' == $text) {
			$text = get_the_content('');
			$text = do_shortcode($text);
			$text = apply_filters('the_content', $text);
			$text = str_replace(']]>', ']]>', $text);
			$excerpt_length = apply_filters('excerpt_length', '200');
			$excerpt_more = apply_filters('excerpt_more', ' [...]');
			$text = wp_trim_words($text, $excerpt_length, $excerpt_more);
		}
		return apply_filters('wp_trim_excerpt', $text, $raw_excerpt);
	}
}
remove_filter('get_the_excerpt', 'wp_trim_excerpt');
add_filter('get_the_excerpt', 'mh_trim_excerpt');

if (!function_exists('mh_excerpt')) {
	function mh_excerpt($excerpt_length = '175') {
		global $options, $post;
		if (!has_excerpt()) {
			$permalink = get_permalink($post->ID);
			$excerpt_more = empty($options['excerpt_more']) ? '[...]' : $options['excerpt_more'];
			$excerpt = get_the_excerpt();
			$excerpt = substr($excerpt, 0, $excerpt_length);
			$excerpt = substr($excerpt, 0, strrpos($excerpt, ' '));
			echo '<div class="mh-excerpt">' . $excerpt . ' <a href="' . $permalink . '" title="' . get_the_title() . '">' . esc_attr($excerpt_more) . '</a></div>' . "\n";
		} else {
			echo esc_attr(the_excerpt());
		}
	}
}

/***** Custom Commentlist *****/

if (!function_exists('mh_comments')) {
	function mh_comments($comment, $args, $depth) {
		$GLOBALS['comment'] = $comment; ?>
		<li <?php comment_class(); ?> id="li-comment-<?php comment_ID() ?>">
			<div id="comment-<?php comment_ID(); ?>">
				<div class="vcard meta">
					<?php echo get_avatar($comment->comment_author_email, 30); ?>
					<?php echo get_comment_author_link() ?> //
					<a href="<?php echo esc_url(get_comment_link($comment->comment_ID)) ?>"><?php printf(__('%1$s at %2$s', 'mh'), get_comment_date(),  get_comment_time()) ?></a> //
					<?php if (comments_open() && $args['max_depth']!=$depth) { ?>
					<?php comment_reply_link(array_merge($args, array('depth' => $depth, 'max_depth' => $args['max_depth']))) ?>
					<?php } ?>
					<?php edit_comment_link(__('(Edit)', 'mh'),'  ','') ?>
				</div>
				<?php if ($comment->comment_approved == '0') : ?>
					<div class="comment-info"><?php _e('Your comment is awaiting moderation.', 'mh') ?></div>
				<?php endif; ?>
				<div class="comment-text">
					<?php comment_text() ?>
				</div>
			</div><?php
	}
}

/***** Custom Comment Fields *****/

if (!function_exists('mh_comment_fields')) {
	function mh_comment_fields($fields) {
		$commenter = wp_get_current_commenter();
		$req = get_option('require_name_email');
		$aria_req = ($req ? " aria-required='true'" : '');
		$fields =  array(
			'author'	=>	'<p class="comment-form-author"><label for="author">' . __('Name ', 'mh') . '</label>' . ($req ? '<span class="required">*</span>' : '') . '<br/><input id="author" name="author" type="text" value="' . esc_attr($commenter['comment_author']) . '" size="30"' . $aria_req . ' /></p>',
			'email' 	=>	'<p class="comment-form-email"><label for="email">' . __('Email ', 'mh') . '</label>' . ($req ? '<span class="required">*</span>' : '' ) . '<br/><input id="email" name="email" type="text" value="' . esc_attr($commenter['comment_author_email']) . '" size="30"' . $aria_req . ' /></p>',
			'url' 		=>	'<p class="comment-form-url"><label for="url">' . __('Website', 'mh') . '</label><br/><input id="url" name="url" type="text" value="' . esc_attr($commenter['comment_author_url']) . '" size="30" /></p>'
		);
		return $fields;
	}
}
add_filter('comment_form_default_fields', 'mh_comment_fields');

/***** Pagination *****/

if (!function_exists('mh_pagination')) {
	function mh_pagination() {
		global $wp_query;
	    $big = 9999;
		echo paginate_links(array('base' => str_replace($big, '%#%', esc_url(get_pagenum_link($big))), 'format' => '?paged=%#%', 'current' => max(1, get_query_var('paged')), 'prev_next' => true, 'prev_text' => __('&laquo;', 'mh'), 'next_text' => __('&raquo;', 'mh'), 'total' => $wp_query->max_num_pages));
	}
}

if (!function_exists('mh_posts_pagination')) {
	function mh_posts_pagination($content) {
		if (is_singular() && is_main_query()) {
			$content .= wp_link_pages(array('before' => '<div class="pagination">', 'after' => '</div>', 'link_before' => '<span class="pagelink">', 'link_after' => '</span>', 'nextpagelink' => __('&raquo;', 'mh'), 'previouspagelink' => __('&laquo;', 'mh'), 'pagelink' => '%', 'echo' => 0));
		}
		return $content;
	}
}
add_filter('the_content', 'mh_posts_pagination', 1);

/***** Load social scripts *****/

if (!function_exists('mh_social_scripts')) {
	function mh_social_scripts() {
		if (is_active_widget('', '', 'mh_facebook')) {
			global $locale;
			echo "<div id='fb-root'></div><script>(function(d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (d.getElementById(id)) return; js = d.createElement(s); js.id = id; js.src = 'https://connect.facebook.net/" . $locale . "/all.js#xfbml=1'; fjs.parentNode.insertBefore(js, fjs); }(document, 'script', 'facebook-jssdk'));</script>" . "\n";
		}
	}
}
add_action('wp_footer', 'mh_social_scripts');

/***** Automatically add rel="prettyPhoto" *****/

if (!function_exists('mh_add_prettyphoto')) {
	if (isset($options['no_prettyphoto']) ? !$options['no_prettyphoto'] : true) {
		function mh_add_prettyphoto($content) {
    		global $post;
			$pattern = "/<a(.*?)href=('|\")(.*?).(bmp|gif|jpeg|jpg|png)('|\")(.*?)>/i";
			$replacement = '<a$1href=$2$3.$4$5 rel="prettyPhoto">';
			$content = preg_replace($pattern, $replacement, $content);
			return $content;
		}
		add_filter('the_content', 'mh_add_prettyphoto');
	}
}

/***** Add Featured Image Size to Media Gallery Selection *****/

if (!function_exists('custom_image_size_choose')) {
	function custom_image_size_choose($sizes) {
		$custom_sizes = array('content' => 'Featured Image');
		return array_merge($sizes, $custom_sizes);
	}
}
add_filter('image_size_names_choose', 'custom_image_size_choose');

/***** Include Functions *****/

if (is_admin()) {
	require_once('admin/admin.php');
}

require_once('includes/mh-options.php');
require_once('includes/mh-widgets.php');

?>