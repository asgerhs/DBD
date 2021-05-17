package dk.cphbusiness.mrv.twitterclone.impl;

import dk.cphbusiness.mrv.twitterclone.contract.PostManagement;
import dk.cphbusiness.mrv.twitterclone.dto.Post;
import dk.cphbusiness.mrv.twitterclone.util.Time;
import redis.clients.jedis.Jedis;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class PostManagementImpl implements PostManagement {
    private Jedis jedis;
    private Time time;

    public PostManagementImpl(Jedis jedis, Time time) {
        this.jedis = jedis;
        this.time = time;
    }

    @Override
    public boolean createPost(String username, String message) {
        boolean user = jedis.hexists(username, "firstname");
        if(!user) return false; 
        long ts = time.getCurrentTimeMillis();
        jedis.hset("post#" + username, "" + ts, message);

        return true; 
    }

    @Override
    public List<Post> getPosts(String username) {
        Map<String, String> posts = jedis.hgetAll("post#" + username);
        List<Post> listPosts = new ArrayList<>();
        for(String ts : posts.keySet()) {
            listPosts.add(new Post(Long.parseLong(ts), posts.get(ts)));
        }
        return listPosts;
    }

    @Override
    public List<Post> getPostsBetween(String username, long timeFrom, long timeTo) {
        Map<String, String> posts = jedis.hgetAll("posts#" + username);
        List<Post> listPosts = new ArrayList<>(); 
        for(String ts : posts.keySet()) {
            if(Long.parseLong(ts) <= timeTo && Long.parseLong(ts) >= timeFrom)
                listPosts.add(new Post(Long.parseLong(ts), posts.get(ts)));
        }
        return listPosts;
    }
}
