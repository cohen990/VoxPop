<?php /* Template Name: Homepage */ ?>
<?php $options = get_option('mh_options'); ?>
<?php get_header(); ?>
<div class="wrapper hp clearfix">
	<div class="clearfix">
		<div class="content left">
		    <?php dynamic_sidebar('home-1'); ?>
			<?php if (is_active_sidebar('home-2') || is_active_sidebar('home-3')) : ?>
			<div class="clearfix">
			<?php if (is_active_sidebar('home-2')) { ?>
		    	<div class="hp-sidebar hp-sidebar-left">
			    	<?php dynamic_sidebar('home-2'); ?>
				</div>
			<?php } ?>
			<?php if (is_active_sidebar('home-3')) { ?>
		    	<div class="hp-sidebar sb-right hp-sidebar-right">
			    	<?php dynamic_sidebar('home-3'); ?>
				</div>
			<?php } ?>
			</div>
			<?php endif; ?>
			<?php dynamic_sidebar('home-4'); ?>
		</div>
		<?php if (is_active_sidebar('home-5')) { ?>
		<div class="hp-sidebar sb-right hp-home-6">
        	<?php dynamic_sidebar('home-5'); ?>     
		</div>
		<?php } ?>
	</div>
</div>
<?php get_footer(); ?>