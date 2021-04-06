import org.json.JSONArray;
import org.json.JSONObject;
import src.generated.valuedomains.*;
import src.generated.valuedomains.impl.JobsBuilderImpl;
import src.generated.valuedomains.impl.JobsImpl;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

public class SyntaxTree {

    public static void main(String[] args) {
        try {
            String content = Files.readString(Paths.get("src\\Data\\data.json"));
            JSONObject data = new JSONObject(content);

            data.getJSONObject("worker").getString("name");

            Id id = Id.create(data.getJSONObject("worker").getInt("id"));
            Name workerName = Name.create(data.getJSONObject("worker").getString("name"));
            Age age = Age.create(data.getJSONObject("worker").getInt("age"));

            JSONArray arr = data.getJSONArray("jobs");
            JobsBuilderImpl jobsBuilder = new JobsBuilderImpl();
            Jobs jobs = new JobsImpl(null);
            for (int i = 0; i < arr.length(); i++) {
                JSONObject jsonJob = arr.getJSONObject(i);
                Name jobName = Name.create(jsonJob.getString("name"));
                Pay pay = Pay.create(jsonJob.getInt("pay"));
                Job job = Job.create().name(jobName).pay(pay);
                jobs = jobsBuilder.add(job).end();
            }

            Worker worker = Worker.create().id(id).name(workerName).age(age);
            Overall overall = Overall.create().worker(worker).jobs(jobs);
            System.out.println(overallToString(overall));
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }
    private static String jobsToString(Jobs jobs) {
        String str = "";
        for(Job a : jobs) {
            str +=  "\n{" +
                    "\n\"name\": " + a.name() + "," +
                    "\n\"pay\": " + a.pay() +
                    "\n},";
        }
        return str;
    }
    private static String overallToString(Overall o) {
        return "{\n\"worker\": " +
                "\n{" +
                "\n\"id\": " + o.worker().id() + "," +
                "\n\"name\": " + o.worker().name() + "," +
                "\n\"age\": " + o.worker().age() +
                "\n}," +
                "\n\"jobs\": [" +
                jobsToString(o.jobs()) +
                "\n]" +
                "\n}";

    }
}
