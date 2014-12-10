<?php /* Comments Template */
if (!empty($_SERVER['SCRIPT_FILENAME']) && 'comments.php' == basename($_SERVER['SCRIPT_FILENAME']))
	die ('Please do not load this page directly. Thanks!');
if (post_password_required()) { ?>
	<p class="no-comments"><?php echo __('This post is password protected. Enter the password to view comments.', 'mh'); ?></p><?php
	return;
}
$comments_by_type = separate_comments($comments);
if (have_comments()) {
	if (!empty($comments_by_type['comment'])) {		
		$comment_count = count($comments_by_type['comment']);
		($comment_count !== 1) ? $comment_text = __('Comments', 'mh') : $comment_text = __('Comment', 'mh'); ?>				
		<h4 class="section-title"><?php echo $comment_count . ' ' . $comment_text . __(' on ', 'mh') . get_the_title(); ?></h4>
		<ol class="commentlist">
			<?php echo wp_list_comments('callback=mh_comments&type=comment'); ?>
		</ol><?php				
	}
	if (get_comments_number() > get_option('comments_per_page')) { ?>
		<div class="comments-pagination">
			<?php paginate_comments_links(array('prev_text' => __('&laquo;', 'mh'), 'next_text' => __('&raquo;', 'mh'))); ?>
		</div><?php
	}
	if (!empty($comments_by_type['pings'])) {
		$pings = $comments_by_type['pings'];
		$ping_count = count($comments_by_type['pings']); ?>
		<h4 class="section-title"><?php echo $ping_count . ' ' . __('Trackbacks & Pingbacks', 'mh'); ?></h4>
		<ol class="pinglist">
        <?php foreach ($pings as $ping) { ?>
			<li class="pings"><?php echo get_comment_author_link($ping); ?></li>
        <?php } ?>
        </ol><?php
	}		
	if (!comments_open()) { ?>
		<p class="no-comments"><?php _e('Comments are closed.', 'mh'); ?></p><?php	
	}
}
if (comments_open()) {       
	$custom_args = array( 
    	'title_reply' => __('Leave a comment', 'mh'), 
        'comment_notes_before' => '<p class="comment-notes">' . __('Your email address will not be published.', 'mh') . '</p>',
        'comment_notes_after'  => '', 
        'comment_field' => '<p class="comment-form-comment"><label for="comment">' . __('Comment', 'mh') . '</label><br/><textarea id="comment" name="comment" cols="45" rows="5" aria-required="true"></textarea></p>');
	comment_form($custom_args);        				
} 
?>