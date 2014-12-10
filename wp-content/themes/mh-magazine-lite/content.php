<?php /* Default template for displaying content. */ ?>
<?php $options = get_option('mh_options'); ?>
<article <?php post_class(); ?>>
	<header class="post-header">			
		<h1 class="entry-title"><?php the_title(); ?></h1>
		<?php mh_post_header(); ?>
		<p class="meta post-meta"><?php _e('Posted on ', 'mh'); ?><span class="updated"><?php the_date(); ?></span><?php _e(' by ', 'mh'); ?><span class="vcard author"><span class="fn"><?php the_author_posts_link(); ?></span></span><?php _e(' in ', 'mh') . the_category(', ') ?>  //  <?php comments_number(__('0 Comments', 'mh'), __('1 Comment', 'mh'), __('% Comments', 'mh'));?></p>
	</header>
	<?php dynamic_sidebar('posts-1'); ?>
	<div class="entry clearfix">
		<?php mh_featured_image(); ?>
		<?php the_content(); ?>
	</div>
	<?php if (has_tag()) : ?>
		<div class="post-tags clearfix">
        	<?php the_tags('<ul><li>','</li><li>','</li></ul>'); ?>
        </div>
	<?php endif; ?>
	<?php dynamic_sidebar('posts-2'); ?>	
</article>